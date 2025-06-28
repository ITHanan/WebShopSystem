using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Models;

namespace ApplicationLayer.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CourseDto> GetAvailableCourses()
        {
            var courses = _context.Courses
                .Where(c => c.StartDate > DateTime.Now)
                .Select(c => new CourseDto
                {
                    Name = c.Name,
                    Level = c.Level.ToString(),
                    Time = c.Time.ToString("HH:mm"),
                    Location = c.Location
                })
                .ToList();

            return courses;
        }
    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
    }
}
