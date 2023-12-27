using Crochet.Application.Models;

namespace Crochet.Application.Services;

public interface IPostService
{
    Task<bool> CreateAsync(Post post);
    Task<Post?> GetByIdAsync(Guid id);
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> UpdateAsync(Post post);
    Task<bool> DeleteByIdAsync(Guid id);
}
