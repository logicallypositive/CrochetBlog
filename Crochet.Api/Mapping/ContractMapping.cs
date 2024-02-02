using Crochet.Application.Models;
using Crochet.Contracts.Requests;
using Crochet.Contracts.Responses;

namespace Crochet.Api.Mapping;

public static class ContractMapping
{
    public static Post MapToPost(this CreatePostRequest request)
    {
        return new Post
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Rating = request.Rating,
            Description = request.Description,
            Category = request.Category.ToList(),
            DateAdded = DateTime.UtcNow,
            ImageUrl = request.ImageUrl
        };
    }

    public static PostResponse MapToResponse(this Post post)
    {
        return new PostResponse
        {
            Id = post.Id,
            Title = post.Title,
            Rating = post.Rating,
            Description = post.Description,
            Category = post.Category.ToList(),
            DateAdded = post.DateAdded,
            ImageUrl = post.ImageUrl
        };
    }

    public static PostsResponse MapToResponse(this IEnumerable<Post> posts)
    {
        return new PostsResponse { Items = posts.Select(MapToResponse) };
    }

    public static Post MapToPost(this UpdatePostRequest request, Guid id)
    {
        return new Post
        {
            Id = id,
            Title = request.Title,
            Rating = request.Rating,
            Description = request.Description,
            Category = request.Category.ToList(),
            DateAdded = DateTime.UtcNow,
            ImageUrl = request.ImageUrl
        };
    }
}
