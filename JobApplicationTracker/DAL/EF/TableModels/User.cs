using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Password { get; set; }

        // Foreign key for Role
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Role Role { get; set; } // Navigation property for Role

        // Navigation property for Job Applications
        public virtual ICollection<JobApplication> JobApplications { get; set; }

        public User()
        {
            JobApplications = new List<JobApplication>();
        }
    }
}
