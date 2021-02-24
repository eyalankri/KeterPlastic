 
using System;
 

namespace Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }        
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }        
        public decimal ProductPriceUsd { get; set; } 
        public string ProductFileName { get; set; }
        public DateTime  DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}
