using ApplicationLayer.Bookings.DTOs;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.Bookings.Queries.GetBookingDetails
{
    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, OperationResult<BookingDetailsDto>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingDetailsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<OperationResult<BookingDetailsDto>> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingResult = await _bookingRepository.GetBookingByIdAsync(request.BookingId);

                if (!bookingResult.IsSuccess || bookingResult.Data == null)
                    return OperationResult<BookingDetailsDto>.Failure(bookingResult.ErrorMessage ?? "Booking not found.");

                var booking = bookingResult.Data;

                // Authorization check: Ensure booking belongs to the user's branch
                if (booking.BranchId != request.UserBranchId)
                {
                    return OperationResult<BookingDetailsDto>.Failure("You do not have access to this booking.");
                }

                var bookingDto = new BookingDetailsDto
                {
                    BookingId = booking.BookingId,
                    VehiclePlateNumber = booking.VehiclePlateNumber,
                    ServiceType = booking.ServiceType,
                    AppointmentDate = booking.AppointmentDate,
                    Status = booking.Status,
                    CustomerName = booking.CustomerName,
                    CustomerPhone = booking.CustomerPhone,
                    CustomerEmail = booking.CustomerEmail,
                    Notes = booking.Notes,
                    BranchId = booking.BranchId,
                    BranchName = booking.Branch?.Name,
                    CreatedAt = booking.CreatedAt
                };

                return OperationResult<BookingDetailsDto>.Success(bookingDto);
            }
            catch (Exception ex)
            {
                return OperationResult<BookingDetailsDto>.Failure($"Error retrieving booking details: {ex.Message}");
            }
        }
    }
}
