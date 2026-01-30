using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    internal class BookingRepository : IBookingRepository
    {
        private readonly WebShopSystemDbContext _context;

        public BookingRepository(WebShopSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<Booking>>> GetBookingsByBranchIdAsync(int branchId)
        {
            try
            {
                var bookings = await _context.Bookings
                    .Where(b => b.BranchId == branchId)
                    .Include(b => b.Branch)
                    .AsNoTracking()
                    .ToListAsync();

                return OperationResult<IEnumerable<Booking>>.Success(bookings);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Booking>>.Failure($"Error retrieving bookings: {ex.Message}");
            }
        }

        public async Task<OperationResult<Booking>> GetBookingByIdAsync(int bookingId)
        {
            try
            {
                var booking = await _context.Bookings
                    .Include(b => b.Branch)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.BookingId == bookingId);

                if (booking == null)
                    return OperationResult<Booking>.Failure("Booking not found.");

                return OperationResult<Booking>.Success(booking);
            }
            catch (Exception ex)
            {
                return OperationResult<Booking>.Failure($"Error retrieving booking: {ex.Message}");
            }
        }
    }
}
