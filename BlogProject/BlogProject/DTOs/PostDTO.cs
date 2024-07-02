using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string TagData { get; set; }
        [Required]
        public string PostBody { get; set; }
        public System.DateTime PostCreated { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PostTagDTO> PostTags { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }

        public PostDTO() { 
            PostTags = new List<PostTagDTO>();
            Comments = new List<CommentDTO>();
        }

    }
}