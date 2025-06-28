using ApplicationLayer.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebLayer.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var courses = _courseService.GetAvailableCourses();
            return View(courses);
        }
    }

  
}
