using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UName { get; set; }
        public string Pass { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
    }
}