namespace Domain.Models;

public class TeamMember
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public int PlayerId { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LeftAt { get; set; } // moze byc nullem bo dopoki nie opusci druzyny to bedzie null 
    public Team Team { get; set; } = null!;
    public Player Player { get; set; } = null!;
}