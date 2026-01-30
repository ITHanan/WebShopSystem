using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        [MaxLength(20)]
        public string VehiclePlateNumber { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ServiceType { get; set; } = null!;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Assigned, InProgress, Completed, Cancelled

        public string? CustomerName { get; set; }

        public string? CustomerPhone { get; set; }

        public string? CustomerEmail { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        [Required]
        public int BranchId { get; set; }

        // Navigation properties
        public Branch? Branch { get; set; }
    }
}
