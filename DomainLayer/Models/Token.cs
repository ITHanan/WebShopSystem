using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Token
    {
        public int TokenID { get; set; }
        public int UserID { get; set; }

        public User? User { get; set; }
        public string? AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }


    }
}