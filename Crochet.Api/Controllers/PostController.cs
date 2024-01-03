using Crochet.Api.Mapping;
using Crochet.Application.Services;
using Crochet.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Crochet.Api.Controllers;

[ApiController]
public class PostController(IPostService postService) : ControllerBase
{
    [HttpPost(ApiEndpoints.Posts.Create)]
    public async Task<IActionResult> Create(
        [FromBody] CreatePostRequest request,
        CancellationToken token
    )
    {
        var post = request.MapToPost();
        await postService.CreateAsync(post, token);
        return Created($"/{ApiEndpoints.Posts.Create}{post.Id}", post);
    }

    [HttpGet(ApiEndpoints.Posts.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var post = await postService.GetByIdAsync(id, token);

        if (post is null)
        {
            return NotFound();
        }

        var response = post.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Posts.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var posts = await postService.GetAllAsync(token);

        var postsResponse = posts.MapToResponse();

        return Ok(postsResponse);
    }

    [HttpPut(ApiEndpoints.Posts.Update)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdatePostRequest request,
        CancellationToken token
    )
    {
        var post = request.MapToPost(id);
        var updatedPost = await postService.UpdateAsync(post, token);

        if (updatedPost is null)
        {
            return NotFound();
        }

        var response = updatedPost.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Posts.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var deleted = await postService.DeleteByIdAsync(id, token);

        if (!deleted)
        {
            return NotFound();
        }
        return Ok();
    }
}
