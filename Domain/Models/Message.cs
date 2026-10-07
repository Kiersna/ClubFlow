namespace Domain.Models;

public class Message
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int SenderId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Club Club { get; set; } = null!;
    public User Sender { get; set; } = null!;
}