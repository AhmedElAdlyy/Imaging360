using Microsoft.EntityFrameworkCore;
using PostsAndComments.Models;
using PostsAndComments.Services.Interface;
using PostsAndComments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Implementation
{
    public class CommentDb : IComment
    {

        private BlogContext _db;

        public CommentDb(BlogContext db)
        {
            _db = db;
        }

        public MessageResponseViewModel AddComment(CommentViewModel newComment)
        {
            List<Comment> replies = new List<Comment>();

            if (newComment.ParentCommentId != null)
                replies = GetChildrenComments(newComment.ParentCommentId.Value);


            if (replies.Count() > 0)
                newComment.ParentCommentId = replies[0].CommentId;


            Comment comment = new Comment
            {
                Content = newComment.Content,
                CreatedAt = DateTime.Now,
                UserId = newComment.UserId,
                CommentId = newComment.ParentCommentId,
                PostId = newComment.PostId
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return new MessageResponseViewModel
            {
                IsSuccess = true,
                Message = "Comment add successfully"
            };
            //try
            //{

            //}
            //catch (Exception)
            //{
            //    return new MessageResponseViewModel
            //    {
            //        IsSuccess = false,
            //        Message = "something went wrong try again"
            //    };
            //}
        }

        public MessageResponseViewModel DeleteComment(int commentId)
        {
            var comment = _db.Comments.Find(commentId);
            if (comment == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Comment is Not Found"
                };

            var replies = _db.Comments.Where(w => w.CommentId == commentId).ToList();

            try
            {
                foreach (var reply in replies)
                {
                    _db.Comments.Remove(reply);
                }

                _db.Comments.Remove(comment);
                _db.SaveChanges();

                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "Deleted successfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "something went wrong try again later"
                };
            }

        }

        public MessageResponseViewModel EditComment(int commentId, CommentViewModel editComment)
        {
            var comment = _db.Comments.Find(commentId);

            try
            {

                comment.Content = editComment.Content;
                comment.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "Comment updated successfully"
                };
            }
            catch (Exception)
            {

                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Something went wrong try again"
                };
            }
        }

        public List<Comment> ViewAllComments()
        {
            return _db.Comments.ToList();
        }

        public Comment ViewDetails(int id)
        {
            return _db.Comments.Include(i => i.Replies).FirstOrDefault(f => f.Id == id);
        }

        private List<Comment> GetChildrenComments(int parentId)
        {
            return _db.Comments.Where(w => w.CommentId == parentId).ToList();
        }
    }
}
