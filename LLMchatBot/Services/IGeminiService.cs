namespace GeminiChatBot.Services;

/// <summary>
/// Gemini API servis interface'i
/// </summary>
public interface IGeminiService
{
    /// <summary>
    /// Kullanici mesajina karsilik Gemini API'den cevap alir
    /// </summary>
    Task<string> GetResponseAsync(string userMessage);
}

