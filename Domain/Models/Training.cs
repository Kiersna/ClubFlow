using Domain.Enums;
namespace Domain.Models;

public class Training
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int TeamId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public TrainingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public Club Club { get; set; } = null!;
    public Team Team { get; set; } = null!;
    public User Creator { get; set; } = null!;
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Absence> Absences { get; set; } = new List<Absence>();
}