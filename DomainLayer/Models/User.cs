using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Role { get; set; } = "Participant"; // Default role


        // ? Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}