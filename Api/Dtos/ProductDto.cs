using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
       
        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductPriceUsd { get; set; }


        
        public IFormFile ProductPhoto { get; set; }


        public string ProductFileName { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }



    }
}
