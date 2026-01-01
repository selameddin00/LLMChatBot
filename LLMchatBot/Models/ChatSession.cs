namespace GeminiChatBot.Models;

/// <summary>
/// Sohbet oturumunu temsil eden model
/// </summary>
public class ChatSession
{
    public string SessionTitle { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<ChatMessage> Messages { get; set; } = new();
}

