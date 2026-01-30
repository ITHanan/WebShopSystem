using System;

namespace ApplicationLayer.Bookings.DTOs
{
    public class BookingDetailsDto
    {
        public int BookingId { get; set; }
        public string VehiclePlateNumber { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? Notes { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
