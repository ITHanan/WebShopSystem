using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Courses.DTOs
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Level { get; set; }
        public string? Language { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxParticipants { get; set; }
        public string? Location { get; set; }
        public string? TeacherName { get; set; }
        public int MaterialCount { get; set; }
    }
}
