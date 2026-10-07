namespace Domain.Models;
public class Team
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public Club Club { get; set; } = null!;
    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    public ICollection<TeamCoach> TeamCoaches { get; set; } = new List<TeamCoach>();
    public ICollection<Training> Trainings { get; set; } = new List<Training>();
}