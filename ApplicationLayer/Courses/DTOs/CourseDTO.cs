using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Courses.DTOs
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string? Language { get; set; }
        public string? FlagUrl { get; set; }
      //  public string? Name { get; set; }
        public string? Description { get; set; }
        public int Lessons { get; set; }
        public int VideoCount { get; set; }
        public int PdfCount { get; set; }
        public int AudioCount { get; set; }

        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public int MaxParticipants { get; set; }
        //public string? Location { get; set; }
        //public string? TeacherName { get; set; }
        //public int MaterialCount { get; set; }
    }
}
