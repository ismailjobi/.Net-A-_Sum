using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Company { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Position { get; set; }

        [Required]
        public DateTime DateApplied { get; set; }

        // Status of the application
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Status { get; set; }

        // Foreign key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; } // Navigation property for User

        // Navigation property for status history, notes, and reminder
        public virtual ICollection<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
        public virtual ICollection<ApplicationNote> ApplicationNotes { get; set; }
        
        public JobApplication()
        {
            ApplicationStatusHistories = new List<ApplicationStatusHistory>();
            ApplicationNotes = new List<ApplicationNote>();
        }
    }
}
