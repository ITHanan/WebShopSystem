using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class CourseMaterial
    {
        public int MaterialID { get; set; }
        public int CourseID { get; set; }

        public Course? Course { get; set; }
        public string? Title { get; set; }
        public string? FileURL { get; set; }
        public DateTime? UploadAt { get; set; }
        public string? Description { get; set; }

        public string? VideoURL { get; set; }
        public string? AudioURL { get; set; }
    }
}