namespace Domain.Models;

public class AuditLog
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int? UserId { get; set; } 
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? Metadata { get; set; }
    public Club Club { get; set; } = null!; 
    public User? User { get; set; } 
}