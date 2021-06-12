using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string Content { get; set; }
        public string UserId { get; set; }
        [Required]
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
