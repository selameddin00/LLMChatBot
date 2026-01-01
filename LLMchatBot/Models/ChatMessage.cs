namespace GeminiChatBot.Models;

/// <summary>
/// Sohbet mesajini temsil eden model
/// </summary>
public class ChatMessage
{
    public string Content { get; set; } = string.Empty;
    public bool IsUser { get; set; }
    public bool IsError { get; set; }
    public DateTime Timestamp { get; set; }
}

