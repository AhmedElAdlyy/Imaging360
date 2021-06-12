using Microsoft.AspNetCore.Authorization;
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
    public class PostDb : IPost
    {
        private BlogContext _db;

        public PostDb(BlogContext db)
        {
            _db = db;
        }

        public MessageResponseViewModel CreateNewPost(PostViewModel newPost, string userId)
        {

            Post post = new Post
            {
                Content = newPost.Content,
                Title = newPost.Title,
                CreatedAt = DateTime.Now,
                UserId = userId,
            };

            try
            {
                _db.Posts.Add(post);
                _db.SaveChanges();

                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "your Post is published successfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "something went wrong try again"
                };
            }


        }

        public MessageResponseViewModel DeletePost(int postId)
        {
            var post = _db.Posts.Find(postId);

            if (post == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Post not found"
                };

            try
            {
                _db.Posts.Remove(post);
                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "post deleted successfully"
                };
            }
            catch (Exception)
            {

                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "something went wrong try again"
                };
            }
        }

        public MessageResponseViewModel EditPost(int postId, PostViewModel editPost)
        {
            var post = _db.Posts.Find(postId);
            if (post == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Post not found"
                };

            try
            {
                post.Content = editPost.Content;
                post.Title = editPost.Title;
                post.EditedAt = DateTime.Now;

                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "post Updated sucessfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "something went wrong try again"
                };

            }
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            return _db.Posts.Include(i => i.Comments).FirstOrDefault(f => f.Id == id);
        }

    }
}
