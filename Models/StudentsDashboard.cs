using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.CustomValidation;

namespace WebApplication1.Models
{
    public class StudentsDashboard
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage =" provide Name")]
        [MaxLength(25, ErrorMessage ="Max 25 Character")]
        [MinLength(5, ErrorMessage = "Min 5 Character")]
        public string Name { get; set; }

        [Required(ErrorMessage = " provide Email")]
        [MaxLength(25, ErrorMessage = "Max 30 Character")]
        [MinLength(5, ErrorMessage = "Min 5 Character")]
        public string Email { get; set; }


        [Required(ErrorMessage = " provide Password")]
        [MaxLength(25, ErrorMessage = "Max 15 Character")]
        [MinLength(5, ErrorMessage = "Min 5 Character")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }


        [Required(ErrorMessage = "provide Phone number")]
        [RegularExpression("^01[0-9]+", ErrorMessage = "Phone number must start with 01")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone number must be 11 characters long")]
        public String Phone { get; set; }

        [Required(ErrorMessage = "Enter Date of birth")]
        [Min18Year]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Select Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Department")]
        public int DeptID { get; set; }

    }
}