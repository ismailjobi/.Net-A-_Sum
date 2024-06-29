using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Uname { get; set; }
        [Required]
        public string Pass { get; set; }
        public int TypeId { get; set; }
        public virtual TypeDTO Type { get; set; }
    }
}