using Domain.Enums;
namespace Domain.Models;
public class Attendance
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public int PlayerId { get; set; }
    public AttendanceStatus Status { get; set; }
    public DateTime MarkedAt { get; set; }
    public int MarkedBy { get; set; }
    public Training Training { get; set; } = null!;
    public Player Player { get; set; } = null!;
    public User Marker { get; set; } = null!;
}