using DomainLayer.Common;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IBookingRepository
    {
        Task<OperationResult<IEnumerable<Booking>>> GetBookingsByBranchIdAsync(int branchId);
        Task<OperationResult<Booking>> GetBookingByIdAsync(int bookingId);
    }
}
