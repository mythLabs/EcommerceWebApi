using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceWebApi.Entity
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockSize { get; set; }

        public string ProductDescription { get; set; }

        public bool? isDeleted { get; set; }

        public string SellerName { get; set; }


        // Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}