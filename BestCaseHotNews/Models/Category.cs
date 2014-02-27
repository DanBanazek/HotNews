using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestCaseHotNews.Models
{
    public class Category
    {
        public int categoryID { get; set; }
        public string categoryName {get; set;}
        public DateTime dateAdded { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}