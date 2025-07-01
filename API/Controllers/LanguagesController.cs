using ApplicationLayer.LanguageComAndQu.Commands;
using ApplicationLayer.LanguageComAndQu.DTOs;
using ApplicationLayer.LanguageComAndQu.Queries;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            var result = await _mediator.Send(new GetLanguagesQuery());

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }





        [Authorize]
        [HttpPut("set-language")]
        public async Task<IActionResult> SetLanguage([FromBody] SetLanguageDTO setLanguageDTO)
        {
            // Extract the user ID from JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized("Invalid user ID in token.");

            // Create command with userId from token
            var command = new SetUserLanguageCommand
            {
                UserId = userId,
                LanguageId = setLanguageDTO.LanguageId
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            return NoContent();
        }


        /// <summary>
        /// Get filtered and sorted list of languages.
        /// </summary>
        /// <param name="name">Filter by language name</param>
        /// <param name="code">Filter by language code</param>
        /// <param name="sortOrder">Sorting direction (asc/desc)</param>
        /// <param name="sortBy">Field to sort by (Name, Code)</param>
        /// <returns>List of filtered and sorted languages</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredLanguages(
            [FromQuery] string? name,
            [FromQuery] string? code,
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string sortBy = "name")
        {
            var query = new GetFilteredLanguagesQuery
            {
                Name = name,
                Code = code,
                SortOrder = sortOrder,
                SortBy = sortBy
            };

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

    }

}
