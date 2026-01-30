using ApplicationLayer.Bookings.DTOs;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Bookings.Queries.GetBookingDetails
{
    public class GetBookingDetailsQuery : IRequest<OperationResult<BookingDetailsDto>>
    {
        public int BookingId { get; set; }
        public int UserBranchId { get; set; }

        public GetBookingDetailsQuery(int bookingId, int userBranchId)
        {
            BookingId = bookingId;
            UserBranchId = userBranchId;
        }
    }
}
