using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public string? ContactInfo { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}