namespace Domain.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Club Club { get; set; } = null!;
        public ICollection<User> Parents { get; set; } = new List<User>();
        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
