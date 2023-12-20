using Microsoft.AspNetCore.Mvc;
using Crochet.Application.Repositories;
using Crochet.Contracts.Requests;
using Crochet.Api.Mapping;

namespace Crochet.Api.Controllers
{
    [ApiController]
    public class PostController(IPostRepository postRepository) : ControllerBase
    {
        [HttpPost(ApiEndpoints.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = request.MapToPost();
            await postRepository.CreateAsync(post); //CreateAsync(post); //CreateAsync(post);
            return Created($"/{ApiEndpoints.Posts.Create}{post.Id}", post);
        }

        [HttpGet(ApiEndpoints.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await postRepository.GetByIdAsync(id);

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
            var posts = await postRepository.GetAllAsync();

            var postsResponse = posts.MapToResponse();

            return Ok(postsResponse);
        }

        [HttpPut(ApiEndpoints.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = request.MapToPost(id);
            var updated = await postRepository.UpdateAsync(post);

            if (!updated)
            {
                return NotFound();
            }

            var response = post.MapToResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await postRepository.DeleteByIdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

