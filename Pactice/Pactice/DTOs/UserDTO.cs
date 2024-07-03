using Pactice.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pactice.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [RegularExpression(@"^[0-9]{2}-\d{5}-[0-9]{1}$", ErrorMessage = "UserId must be in the format XX-XXXXX-X.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[0-9]{2}-\d{5}-[0-9]@student\.aiub\.edu$", ErrorMessage = "Email must be in the format XX-XXXXX-X@student.aiub.edu.")]
        [CustomEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
        public int RoleId { get; set; }
    }
}