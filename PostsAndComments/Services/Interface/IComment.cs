using PostsAndComments.Models;
using PostsAndComments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Interface
{
    public interface IComment
    {
        MessageResponseViewModel AddComment(CommentViewModel comment);
        MessageResponseViewModel EditComment(int commentId, CommentViewModel comment);
        MessageResponseViewModel DeleteComment(int commentId);
        List<Comment> ViewAllComments();
        Comment ViewDetails(int id);


    }
}
