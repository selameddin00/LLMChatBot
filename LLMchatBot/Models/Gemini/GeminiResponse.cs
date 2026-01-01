using System.Text.Json.Serialization;

namespace GeminiChatBot.Models.Gemini;

/// <summary>
/// Gemini API response modeli
/// </summary>
public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate>? Candidates { get; set; }
}

/// <summary>
/// Candidate modeli
/// </summary>
public class Candidate
{
    [JsonPropertyName("content")]
    public ResponseContent? Content { get; set; }
}

/// <summary>
/// Response content modeli
/// </summary>
public class ResponseContent
{
    [JsonPropertyName("parts")]
    public List<ResponsePart>? Parts { get; set; }
}

/// <summary>
/// Response part modeli (text icerir)
/// </summary>
public class ResponsePart
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

