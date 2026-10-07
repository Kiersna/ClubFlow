using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Role Rola { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
