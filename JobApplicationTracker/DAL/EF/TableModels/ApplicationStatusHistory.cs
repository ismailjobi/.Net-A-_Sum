using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class ApplicationStatusHistory
    {
        public int Id { get; set; }

        // Status history for application
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Status { get; set; }

        // Foreign key for Job Application
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }

        public DateTime ChangedAt { get; set; }

        public virtual JobApplication JobApplication { get; set; } // Navigation property for Job Application

    }
}
