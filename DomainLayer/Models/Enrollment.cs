using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLayer.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal Progress { get; set; }
    }
}