using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using GeminiChatBot.Models.Gemini;

namespace GeminiChatBot.Services;

/// <summary>
/// Gemini API servis implementasyonu
/// </summary>
public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

    public GeminiService()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Kullanici mesajina karsilik Gemini API'den cevap alir
    /// </summary>
    public async Task<string> GetResponseAsync(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
            throw new ArgumentException("Kullanici mesaji bos olamaz", nameof(userMessage));

        // API Key'i Secrets'ten al
        var apiKey = Secrets.GeminiApiKey;
        
        if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "BURAYA_GERCEK_KEYINI_YAZ")
        {
            return "Hata: API Key girilmemiş. Lütfen Secrets.cs dosyasına API Key'inizi ekleyin.";
        }

        var request = new GeminiRequest
        {
            Contents = new List<ContentItem>
            {
                new ContentItem
                {
                    Parts = new List<Part>
                    {
                        new Part { Text = userMessage }
                    }
                }
            }
        };

        var url = $"{ApiUrl}?key={apiKey}";
        
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, request);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                    response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return "Hata: API Key geçersiz veya yetkisiz. Lütfen Secrets.cs dosyasındaki API Key'i kontrol edin.";
                }
                throw new Exception($"API hatasi: {response.StatusCode} - {errorContent}");
            }

            var responseContent = await response.Content.ReadFromJsonAsync<GeminiResponse>();

            if (responseContent?.Candidates == null || 
                responseContent.Candidates.Count == 0 ||
                responseContent.Candidates[0].Content?.Parts == null ||
                responseContent.Candidates[0].Content.Parts.Count == 0)
            {
                return string.Empty;
            }

            var text = responseContent.Candidates[0].Content.Parts[0].Text;
            return text ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API'ye ulasilamadi: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception($"API yaniti parse edilemedi: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Beklenmeyen hata: {ex.Message}", ex);
        }
    }
}

