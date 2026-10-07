namespace Domain.Models;

public class Absence
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public int PlayerId { get; set; }
    public int ReportedBy { get; set; }
    public string Reason { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; }
    public Training Training { get; set; } = null!;
    public Player Player { get; set; } = null!;
    public User Reporter { get; set; } = null!;
}