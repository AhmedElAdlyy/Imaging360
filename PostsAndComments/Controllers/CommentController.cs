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
    public class CommentController : ControllerBase
    {
        private IComment _db;

        public CommentController(IComment db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public ActionResult CreateComment([FromBody]CommentViewModel comment)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            comment.UserId = userId;
            if (ModelState.IsValid)
            {
                var result = _db.AddComment(comment);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);

            }
            return BadRequest("Check your data");
        }


        [HttpPut("{id}")]
        public ActionResult EditComment(int id, CommentViewModel editComment)
        {
            if (ModelState.IsValid)
            {
                var result = _db.EditComment(id, editComment);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Check your data");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment(int id)
        {
            var result = _db.DeleteComment(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("All")]
        public ActionResult GetAllComments()
        {
            var comments = _db.ViewAllComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public ActionResult ViewOneComment(int id)
        {
            var post = _db.ViewDetails(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }
    }
}
