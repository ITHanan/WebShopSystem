using ApplicationLayer.Bookings.DTOs;
using DomainLayer.Common;
using MediatR;
using System.Collections.Generic;

namespace ApplicationLayer.Bookings.Queries.GetBookingsByBranch
{
    public class GetBookingsByBranchQuery : IRequest<OperationResult<IEnumerable<BookingSummaryDto>>>
    {
        public int BranchId { get; set; }

        public GetBookingsByBranchQuery(int branchId)
        {
            BranchId = branchId;
        }
    }
}
