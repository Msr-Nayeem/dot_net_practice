using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        [Required(ErrorMessage = "Please enter the product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the product price.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter the product description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose a category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        public HttpPostedFileBase File { get; set; }

        public string ImageLink { get; set; }
    }
}