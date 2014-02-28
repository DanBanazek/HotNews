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
        [Display(Name="User Name")]
        public string userName { get; set; }
        [Display(Name = "Full Name")]
        public string fullName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Display(Name = "Grant Admin Rights")]
        public bool isSiteAdmin{get; set;}
        
        public DateTime? dateCreated { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}