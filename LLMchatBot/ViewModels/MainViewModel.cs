using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeminiChatBot.Models;
using GeminiChatBot.Services;
using System.Collections.ObjectModel;

namespace GeminiChatBot.ViewModels;

/// <summary>
/// Ana pencere icin ViewModel
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly IGeminiService _geminiService;
    private readonly FileService _fileService;
    private ChatSession? _currentSession;

    [ObservableProperty]
    private ObservableCollection<ChatSession> sessions = new();

    [ObservableProperty]
    private ObservableCollection<ChatMessage> messages = new();

    [ObservableProperty]
    private ChatSession? selectedSession;

    [ObservableProperty]
    private string currentInput = string.Empty;

    [ObservableProperty]
    private bool isSending;

    public MainViewModel()
    {
        _geminiService = new GeminiService();
        _fileService = new FileService();
        _ = InitializeAsync();
    }

    /// <summary>
    /// Uygulama acilista sohbet gecmisini yukler
    /// </summary>
    private async Task InitializeAsync()
    {
        try
        {
            var loadedSessions = await _fileService.LoadSessionsAsync();
            
            if (loadedSessions.Any())
            {
                foreach (var session in loadedSessions)
                {
                    Sessions.Add(session);
                }
                
                // Son oturumu sec
                SelectedSession = Sessions.LastOrDefault();
            }
            else
            {
                // Ilk acilista yeni oturum olustur
                await CreateNewSessionAsync();
            }
        }
        catch (Exception)
        {
            // Hata durumunda yeni oturum olustur
            await CreateNewSessionAsync();
        }
    }

    /// <summary>
    /// Yeni sohbet oturumu olusturur
    /// </summary>
    private async Task CreateNewSessionAsync()
    {
        var newSession = new ChatSession
        {
            SessionTitle = "Yeni Sohbet",
            Date = DateTime.Now,
            Messages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Content = "Merhaba ben Gemini, sana nasil yardim edebilirim?",
                    IsUser = false,
                    IsError = false,
                    Timestamp = DateTime.Now
                }
            }
        };

        Sessions.Add(newSession);
        SelectedSession = newSession;
        _currentSession = newSession;
        
        // Messages koleksiyonunu guncelle
        Messages.Clear();
        foreach (var msg in newSession.Messages)
        {
            Messages.Add(msg);
        }

        await SaveSessionsAsync();
    }

    /// <summary>
    /// Sohbet oturumlarini dosyaya kaydeder
    /// </summary>
    private async Task SaveSessionsAsync()
    {
        try
        {
            await _fileService.SaveSessionsAsync(Sessions);
        }
        catch (Exception ex)
        {
            // Hata durumunda kullaniciya bildir (istege bagli)
            System.Diagnostics.Debug.WriteLine($"Kayit hatasi: {ex.Message}");
        }
    }

    /// <summary>
    /// Mesaj gonder komutu
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSendMessage))]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentInput))
            return;

        // Eger aktif session yoksa yeni olustur
        if (_currentSession == null)
        {
            await CreateNewSessionAsync();
        }

        try
        {
            IsSending = true;

            // Kullanici mesajini ekle
            var userMessage = new ChatMessage
            {
                Content = CurrentInput,
                IsUser = true,
                IsError = false,
                Timestamp = DateTime.Now
            };
            Messages.Add(userMessage);
            
            // Session'a da ekle
            if (_currentSession != null)
            {
                _currentSession.Messages.Add(userMessage);
                
                // Session basligini guncelle (ilk kullanici mesajindan)
                if (_currentSession.Messages.Count(m => m.IsUser) == 1)
                {
                    var title = CurrentInput.Length > 30 
                        ? CurrentInput.Substring(0, 30) + "..." 
                        : CurrentInput;
                    _currentSession.SessionTitle = title;
                }
            }

            // Input'u temizle
            var messageToSend = CurrentInput;
            CurrentInput = string.Empty;

            // Gemini API'den cevap al
            try
            {
                var response = await _geminiService.GetResponseAsync(messageToSend);

                ChatMessage botMessage;
                if (string.IsNullOrWhiteSpace(response))
                {
                    // Bos cevap durumu
                    botMessage = new ChatMessage
                    {
                        Content = "Hata: API'den bos cevap alindi",
                        IsUser = false,
                        IsError = true,
                        Timestamp = DateTime.Now
                    };
                }
                else
                {
                    // Basarili cevap
                    botMessage = new ChatMessage
                    {
                        Content = response,
                        IsUser = false,
                        IsError = false,
                        Timestamp = DateTime.Now
                    };
                }
                
                Messages.Add(botMessage);
                
                // Session'a da ekle
                if (_currentSession != null)
                {
                    _currentSession.Messages.Add(botMessage);
                }
            }
            catch (Exception ex)
            {
                // API hatasi durumunda chat ekranina hata mesaji ekle
                var errorMessage = new ChatMessage
                {
                    Content = $"Hata: API'ye ulasilamadi - {ex.Message}",
                    IsUser = false,
                    IsError = true,
                    Timestamp = DateTime.Now
                };
                Messages.Add(errorMessage);
                
                // Session'a da ekle
                if (_currentSession != null)
                {
                    _currentSession.Messages.Add(errorMessage);
                }
            }

            // Otomatik kaydet
            await SaveSessionsAsync();
        }
        finally
        {
            IsSending = false;
        }
    }

    /// <summary>
    /// Mesaj gonderilebilir mi kontrolu
    /// </summary>
    private bool CanSendMessage()
    {
        return !IsSending && !string.IsNullOrWhiteSpace(CurrentInput);
    }

    /// <summary>
    /// Yeni sohbet baslat komutu
    /// </summary>
    [RelayCommand]
    private async Task NewChatAsync()
    {
        await CreateNewSessionAsync();
    }

    /// <summary>
    /// CurrentInput degistiginde CanExecute'u guncelle
    /// </summary>
    partial void OnCurrentInputChanged(string value)
    {
        SendMessageCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// IsSending degistiginde CanExecute'u guncelle
    /// </summary>
    partial void OnIsSendingChanged(bool value)
    {
        SendMessageCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Secili oturum degistiginde mesajlari yukle
    /// </summary>
    partial void OnSelectedSessionChanged(ChatSession? value)
    {
        _currentSession = value;
        
        Messages.Clear();
        
        if (value != null)
        {
            foreach (var msg in value.Messages)
            {
                Messages.Add(msg);
            }
        }
    }
}

