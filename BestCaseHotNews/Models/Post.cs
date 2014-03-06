using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BestCaseHotNews.Models
{
    public class Post
    {
        public int postID { get; set; }
        public int productID { get; set; }
        public int categoryID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string userName { get; set; }
        public int userID { get; set; }
        public string headline { get; set; }
        public string body { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
         public DateTime datePosted { get; set; }
        public DateTime lastUpdate { get; set; }

        public virtual Product Product{get; set;}
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}