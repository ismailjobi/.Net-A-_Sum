using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.DTOs
{
    public class LoginDTO
    {

        [Required]
        public string Uname { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}