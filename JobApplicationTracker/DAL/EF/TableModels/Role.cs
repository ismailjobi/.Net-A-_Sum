using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.TableModels
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string RoleName { get; set; }

        // Navigation property for Users
        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
