using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class ApplicationNote
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Note { get; set; }

        // Foreign key for Job Application
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual JobApplication JobApplication { get; set; } // Navigation property for Job Application
    }
}
