using ApplicationLayer.Bookings.DTOs;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Bookings.Queries.GetBookingsByBranch
{
    public class GetBookingsByBranchQueryHandler : IRequestHandler<GetBookingsByBranchQuery, OperationResult<IEnumerable<BookingSummaryDto>>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByBranchQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<OperationResult<IEnumerable<BookingSummaryDto>>> Handle(GetBookingsByBranchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingsResult = await _bookingRepository.GetBookingsByBranchIdAsync(request.BranchId);

                if (!bookingsResult.IsSuccess)
                    return OperationResult<IEnumerable<BookingSummaryDto>>.Failure(bookingsResult.ErrorMessage ?? "Failed to retrieve bookings.");

                var bookings = bookingsResult.Data ?? new List<DomainLayer.Models.Booking>();

                // Sort by appointment date ascending
                var sortedBookings = bookings
                    .OrderBy(b => b.AppointmentDate)
                    .Select(b => new BookingSummaryDto
                    {
                        BookingId = b.BookingId,
                        VehiclePlateNumber = b.VehiclePlateNumber,
                        ServiceType = b.ServiceType,
                        AppointmentDate = b.AppointmentDate,
                        Status = b.Status
                    });

                return OperationResult<IEnumerable<BookingSummaryDto>>.Success(sortedBookings);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<BookingSummaryDto>>.Failure($"Error retrieving bookings: {ex.Message}");
            }
        }
    }
}
