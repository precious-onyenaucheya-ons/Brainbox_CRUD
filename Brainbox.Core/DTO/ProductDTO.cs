using Brainbox.Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Core.DTO
{
    public class ProductDTO
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Field cannot be empty, Please enter a Valid Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
