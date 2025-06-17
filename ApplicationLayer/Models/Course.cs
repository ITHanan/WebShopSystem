using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLayer.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public enum CourseLevel
        {
            Beginner,
        Intermediate,
        Advanced
        }
        public string Language { get; set; }
        public DateTime Time { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxParticpants { get; set; }
        public int TeacherID { get; set; }
        public string Location { get; set; }
    }
}