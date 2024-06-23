using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabTask_02.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Course Name.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Credit Hours.")]
        public int CreditHr { get; set; }
        [Required(ErrorMessage = "Please enter Course Type.")]
        public string Type { get; set; }
        
    }
}