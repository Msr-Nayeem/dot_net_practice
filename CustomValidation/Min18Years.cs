
using WebApplication1.Models;
using System;
using System.ComponentModel.DataAnnotations;  
  
namespace WebApplication1.CustomValidation
{
    public class Min18Years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reg = (RegModel)validationContext.ObjectInstance;

            if (reg.Dob == null)
                return new ValidationResult("Date of Birth is required.");

            var age = DateTime.Today.Year - reg.Dob.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Minimum 18 years old.");
        }
    }
}