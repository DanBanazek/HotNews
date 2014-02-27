using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestCaseHotNews.Models
{
    public class User
    {
        public int userID { get; set; }
        [Required]
        public string userName { get; set; }
        
        public string fullName { get; set; }

        [EmailAddress]
        public string email { get; set; }
        
       
        public DateTime? dateCreated { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}