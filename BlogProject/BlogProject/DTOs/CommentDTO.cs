using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string CContent { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public System.DateTime CommentTime { get; set; }
        public virtual UserDTO User { get; set; }
    }
}