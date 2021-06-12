using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }


        [ForeignKey("ParentComment")]
        public int? CommentId { get; set; }
        public virtual Comment ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }

    }
}
