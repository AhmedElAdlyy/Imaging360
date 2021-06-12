using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostsAndComments.Services.Interface;
using PostsAndComments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostsAndComments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private IPost _db;

        public PostController(IPost db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public ActionResult CreatePost([FromBody]PostViewModel newPost)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                var result = _db.CreateNewPost(newPost, userId);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Check your Data");
        }

        [HttpPut("{id}")]
        public ActionResult EditPost(int id, PostViewModel editPost)
        {
            if (ModelState.IsValid)
            {
                var result = _db.EditPost(id, editPost);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("check your data");
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            var result = _db.DeletePost(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("All")]
        public ActionResult ViewAllPosts()
        {
            var posts = _db.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult ViewPOstDetails(int id)
        {
            var post = _db.GetPostById(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

    }
}
