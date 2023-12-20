using Microsoft.AspNetCore.Mvc;
using Crochet.Application.Services;
using Crochet.Contracts.Requests;
using Crochet.Api.Mapping;

namespace Crochet.Api.Controllers
{
    [ApiController]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpPost(ApiEndpoints.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = request.MapToPost();
            await postService.CreateAsync(post); //CreateAsync(post); //CreateAsync(post);
            return Created($"/{ApiEndpoints.Posts.Create}{post.Id}", post);
        }

        [HttpGet(ApiEndpoints.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await postService.GetByIdAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            var response = post.MapToResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await postService.GetAllAsync();

            var postsResponse = posts.MapToResponse();

            return Ok(postsResponse);
        }

        [HttpPut(ApiEndpoints.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = request.MapToPost(id);
            var updatedPost = await postService.UpdateAsync(post);

            if (updatedPost is null)
            {
                return NotFound();
            }

            var response = updatedPost.MapToResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await postService.DeleteByIdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

