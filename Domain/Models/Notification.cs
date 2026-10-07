using Domain.Enums;

namespace Domain.Models;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public User User { get; set; } = null!;
}