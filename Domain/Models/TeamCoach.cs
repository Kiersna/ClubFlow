namespace Domain.Models;

public class TeamCoach
{
    public int TeamId { get; set; }
    public int CoachId { get; set; }
    public DateTime AssignedAt { get; set; }
    public Team Team { get; set; } = null!;
    public User Coach { get; set; } = null!;
}