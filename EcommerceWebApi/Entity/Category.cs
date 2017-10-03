using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceWebApi.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDescription { get; set; }
        public bool? isDeleted { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}