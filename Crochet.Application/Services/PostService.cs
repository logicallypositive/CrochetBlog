using Crochet.Application.Models;
using Crochet.Application.Repositories;
using FluentValidation;

namespace Crochet.Application.Services;

public class PostService(IPostRepository postRepository, IValidator<Post> postValidator)
    : IPostService
{
    public async Task<bool> CreateAsync(Post post, CancellationToken token = default)
    {
        await postValidator.ValidateAndThrowAsync(post, cancellationToken: token);
        return await postRepository.CreateAsync(post, token);
    }

    public Task<Post?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return postRepository.GetByIdAsync(id, token);
    }

    public Task<IEnumerable<Post>> GetAllAsync(CancellationToken token = default)
    {
        return postRepository.GetAllAsync(token);
    }

    public async Task<Post?> UpdateAsync(Post post, CancellationToken token = default)
    {
        await postValidator.ValidateAndThrowAsync(post, cancellationToken: token);
        var postExists = await postRepository.ExistsByIdAsync(post.Id, token);
        if (!postExists)
        {
            return null;
        }
        await postRepository.UpdateAsync(post, token);
        return post;
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return postRepository.DeleteByIdAsync(id, token);
    }
}
