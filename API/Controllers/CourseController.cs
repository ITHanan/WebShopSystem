using ApplicationLayer.Courses.Queries;
using ApplicationLayer.LanguageComAndQu.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get-All-Courses")]
        public async Task<ActionResult> GetAllCourses() 
        {
            var result = await _mediator.Send(new GetCoursesQuery());
            return Ok(result);
        }
    }
}
