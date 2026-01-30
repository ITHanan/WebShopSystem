using ApplicationLayer.Bookings.Queries.GetBookingsByBranch;
using ApplicationLayer.Bookings.Queries.GetBookingDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ShopManager")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IMediator mediator, ILogger<BookingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private int? GetUserBranchId()
        {
            var branchIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BranchId");
            if (branchIdClaim == null)
            {
                return null;
            }

            if (int.TryParse(branchIdClaim.Value, out int branchId))
            {
                return branchId;
            }

            return null;
        }

        /// <summary>
        /// Get all bookings for the shop manager's assigned branch
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var branchId = GetUserBranchId();
                if (!branchId.HasValue)
                {
                    _logger.LogWarning("Shop manager {UserId} attempted to access bookings without branch assignment", 
                        User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    return BadRequest(new { message = "Shop manager is not assigned to any branch." });
                }

                var query = new GetBookingsByBranchQuery(branchId.Value);
                var result = await _mediator.Send(query);

                if (!result.IsSuccess)
                {
                    _logger.LogError("Failed to retrieve bookings for branch {BranchId}: {Error}", 
                        branchId.Value, result.ErrorMessage);
                    return BadRequest(new { message = "Failed to retrieve bookings." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving bookings");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Get details of a specific booking (with authorization check)
        /// </summary>
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingDetails(int bookingId)
        {
            if (bookingId <= 0)
            {
                return BadRequest(new { message = "Invalid booking ID." });
            }

            try
            {
                var branchId = GetUserBranchId();
                if (!branchId.HasValue)
                {
                    _logger.LogWarning("Shop manager {UserId} attempted to access booking {BookingId} without branch assignment", 
                        User.FindFirst(ClaimTypes.NameIdentifier)?.Value, bookingId);
                    return BadRequest(new { message = "Shop manager is not assigned to any branch." });
                }

                var query = new GetBookingDetailsQuery(bookingId, branchId.Value);
                var result = await _mediator.Send(query);

                if (!result.IsSuccess)
                {
                    // Check if it's an unauthorized access attempt
                    if (result.ErrorMessage?.Contains("do not have access") == true)
                    {
                        _logger.LogWarning("Shop manager {UserId} from branch {BranchId} attempted unauthorized access to booking {BookingId}", 
                            User.FindFirst(ClaimTypes.NameIdentifier)?.Value, branchId.Value, bookingId);
                        return StatusCode(403, new { message = "You do not have access to this booking." });
                    }

                    _logger.LogWarning("Booking {BookingId} not found", bookingId);
                    return NotFound(new { message = "Booking not found." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving booking {BookingId}", bookingId);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
