using ApplicationLayer.Bookings.Queries.GetBookingsByBranch;
using ApplicationLayer.Bookings.Queries.GetBookingDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all bookings for the shop manager's assigned branch
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                // Extract BranchId from the JWT claims
                var branchIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BranchId");
                if (branchIdClaim == null)
                {
                    return BadRequest(new { message = "Shop manager is not assigned to any branch." });
                }

                if (!int.TryParse(branchIdClaim.Value, out int branchId))
                {
                    return BadRequest(new { message = "Invalid branch assignment." });
                }

                var query = new GetBookingsByBranchQuery(branchId);
                var result = await _mediator.Send(query);

                if (!result.IsSuccess)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get details of a specific booking (with authorization check)
        /// </summary>
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingDetails(int bookingId)
        {
            try
            {
                // Extract BranchId from the JWT claims
                var branchIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BranchId");
                if (branchIdClaim == null)
                {
                    return BadRequest(new { message = "Shop manager is not assigned to any branch." });
                }

                if (!int.TryParse(branchIdClaim.Value, out int branchId))
                {
                    return BadRequest(new { message = "Invalid branch assignment." });
                }

                var query = new GetBookingDetailsQuery(bookingId, branchId);
                var result = await _mediator.Send(query);

                if (!result.IsSuccess)
                {
                    // Check if it's an unauthorized access attempt
                    if (result.ErrorMessage?.Contains("do not have access") == true)
                    {
                        return Forbid();
                    }

                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
