using System;

namespace ApplicationLayer.Bookings.DTOs
{
    public class BookingSummaryDto
    {
        public int BookingId { get; set; }
        public string VehiclePlateNumber { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
