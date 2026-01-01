using System.Text.Json.Serialization;

namespace GeminiChatBot.Models.Gemini;

/// <summary>
/// Gemini API request modeli
/// </summary>
public class GeminiRequest
{
    [JsonPropertyName("contents")]
    public List<ContentItem> Contents { get; set; } = new();
}

/// <summary>
/// Content item modeli
/// </summary>
public class ContentItem
{
    [JsonPropertyName("parts")]
    public List<Part> Parts { get; set; } = new();
}

/// <summary>
/// Part modeli (text icerir)
/// </summary>
public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

