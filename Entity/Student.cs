//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Entity
{
    using System;
    using System.Collections.Generic;
    using WebApplication1.CustomValidation;
    using System.ComponentModel.DataAnnotations;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.CourseStudents = new HashSet<CourseStudent>();
        }

        public int Id { get; set; }


        [Required(ErrorMessage = " provide Name")]
        [MaxLength(25, ErrorMessage = "Max 25 Character")]
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



        [Required(ErrorMessage = "provide Phone number")]
        [RegularExpression("^01[0-9]+", ErrorMessage = "Phone number must start with 01")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone number must be 11 characters long")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Select Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Date of birth")]
        [Min18Year]
        public System.DateTime Dob { get; set; }


        [Required(ErrorMessage = "Select Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Department")]
        public int DeptId { get; set; }

        public virtual Dept Dept { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
