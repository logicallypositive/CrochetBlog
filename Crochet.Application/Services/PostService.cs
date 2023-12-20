using Crochet.Application.Models;
using Crochet.Application.Repositories;

namespace Crochet.Application.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        public Task<bool> CreateAsync(Post post)
        {
            return postRepository.CreateAsync(post);
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
}
