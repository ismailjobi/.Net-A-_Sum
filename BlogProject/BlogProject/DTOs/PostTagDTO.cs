using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.DTOs
{
    public class PostTagDTO
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int PostId { get; set; }
        public virtual PostDTO Post { get; set; }
        public virtual TagDTO Tag { get; set; }
    }
}