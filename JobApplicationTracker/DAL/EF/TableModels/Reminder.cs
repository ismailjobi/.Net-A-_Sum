using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class Reminder
    {
        public int Id { get; set; }

        public DateTime ReminderDate { get; set; }

        public bool IsSent { get; set; }

        // Foreign key for Job Application
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }

       
        public virtual JobApplication JobApplication { get; set; } // Navigation property for Job Application

        
    }
}
