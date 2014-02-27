using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestCaseHotNews.Models
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public DateTime dateCreated { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
 
    }
}