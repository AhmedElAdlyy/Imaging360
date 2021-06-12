using PostsAndComments.Models;
using PostsAndComments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Interface
{
    public interface IPost
    {
        List<Post> GetAllPosts();
        Post GetPostById(int id);
        MessageResponseViewModel CreateNewPost(PostViewModel post, string userId);
        MessageResponseViewModel EditPost(int postId, PostViewModel post);
        MessageResponseViewModel DeletePost(int postId);
    }
}
