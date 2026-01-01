using System.IO;
using System.Text.Json;
using GeminiChatBot.Models;

namespace GeminiChatBot.Services;

/// <summary>
/// Sohbet gecmisi dosya islemleri icin servis
/// </summary>
public class FileService
{
    private const string FileName = "chat_history.json";
    private readonly string _filePath;

    public FileService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appFolder = Path.Combine(appDataPath, "GeminiChatBot");
        
        if (!Directory.Exists(appFolder))
        {
            Directory.CreateDirectory(appFolder);
        }

        _filePath = Path.Combine(appFolder, FileName);
    }

    /// <summary>
    /// Sohbet oturumlarini JSON dosyasina kaydeder
    /// </summary>
    public async Task SaveSessionsAsync(IEnumerable<ChatSession> sessions)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            var json = JsonSerializer.Serialize(sessions, options);
            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            throw new Exception($"Sohbet gecmisi kaydedilirken hata olustu: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sohbet oturumlarini JSON dosyasindan yukler
    /// </summary>
    public async Task<IEnumerable<ChatSession>> LoadSessionsAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<ChatSession>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<ChatSession>();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            var sessions = JsonSerializer.Deserialize<List<ChatSession>>(json, options);
            return sessions ?? new List<ChatSession>();
        }
        catch (JsonException ex)
        {
            throw new Exception($"Sohbet gecmisi dosyasi okunurken hata olustu: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Sohbet gecmisi yuklenirken hata olustu: {ex.Message}", ex);
        }
    }
}

