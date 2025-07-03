

namespace DomainLayer.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public CourseLevel Level { get; set; }

        public int LanguageId { get; set; }         // Foreign key
        public Language? Language { get; set; }      // Navigation property        public DateTime Time { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int MaxParticipants { get; set; }
        public int TeacherID { get; set; }
        public Teacher? Teacher { get; set; }

        public string? Location { get; set; }

        public int Lessons { get; set; }
        public int VideoCount { get; set; }
        public int PdfCount { get; set; }
        public int AudioCount { get; set; }


        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<CourseMaterial>? Materials { get; set; }
    }
}