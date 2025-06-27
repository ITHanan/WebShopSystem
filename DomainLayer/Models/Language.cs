using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Language
    {
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Code { get; set; } // "en", "fr", "hi"

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } // English, French, Hindi

        [MaxLength(255)]
        public string? FlagUrl { get; set; } // e.g. "/images/flags/en.png"

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
