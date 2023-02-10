using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [MaxLength(25, ErrorMessage = "Max 14 Character")]
        [MinLength(5, ErrorMessage = "Min 11 Character")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Enter Date of birth")]
        public DateTime Dob { get; set; }
        

    }
}