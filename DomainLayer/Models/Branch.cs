using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<User>? Users { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
