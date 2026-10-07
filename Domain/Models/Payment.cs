using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Models;
public class Payment
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public int PlayerId { get; set; }
    public string Period { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Club Club { get; set; } = null!;
    public Player Player { get; set; } = null!;
}