using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.DTOs
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UName { get; set; }
        [Required]
        public string Pass { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
    }
}