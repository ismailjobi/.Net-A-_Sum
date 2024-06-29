using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.DTOs
{
    public class TypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Type1 { get; set; }
    }
}