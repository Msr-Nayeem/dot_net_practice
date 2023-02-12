using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.CustomValidation;
namespace WebApplication1.Models
{
    public class RegModel
    {
        [Required(ErrorMessage = " provide Id")]
        [MaxLength(25, ErrorMessage = "Max 25 Character")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
        public String Id { get; set; }


        [Required(ErrorMessage = " provide Name")]
        [MaxLength(25, ErrorMessage = "Max 25 Character")]
        [MinLength(5, ErrorMessage = "Min 5 Character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select Date of Birth")]
        [Min18Years]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Select at least one")]
        public string[] Hobbies { get; set; }

        [Required(ErrorMessage = "Select Profession")]
        public string Profession { get; set; }
    }
}