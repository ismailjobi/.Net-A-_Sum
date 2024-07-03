using Pactice.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pactice.CustomValidation
{
    public class CustomEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserDTO)validationContext.ObjectInstance;
            string id = user.UserId;
            string email = user.Email;

            // Extract the part before '@' from the email
            if (email != null) {
                var emailIdPart = email.Split('@')[0];

                if (id.Equals(emailIdPart))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The ID must match the userid part of the Email.");
                }
            }
            return new ValidationResult("Email is required.");
        }
    }
}