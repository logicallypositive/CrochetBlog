using Crochet.Application.Models;
using Crochet.Application.Repositories;
using FluentValidation;

namespace Crochet.Application.Services;

public class PostService(IPostRepository postRepository, IValidator<Post> postValidator)
    : IPostService
{
    public async Task<bool> CreateAsync(Post post)
    {
        await postValidator.ValidateAndThrowAsync(post);
        return await postRepository.CreateAsync(post);
    }

    public Task<Post?> GetByIdAsync(Guid id)
    {
        return postRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return postRepository.GetAllAsync();
    }

    public async Task<Post?> UpdateAsync(Post post)
    {
        await postValidator.ValidateAndThrowAsync(post);
        var postExists = await postRepository.ExistsByIdAsync(post.Id);
        if (!postExists)
        {
            return null;
        }
        await postRepository.UpdateAsync(post);
        return post;
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        return postRepository.DeleteByIdAsync(id);
    }
}
