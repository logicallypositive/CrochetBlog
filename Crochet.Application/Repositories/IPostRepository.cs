using Crochet.Application.Models;

namespace Crochet.Application.Repositories;

public interface IPostRepository
{
    Task<bool> CreateAsync(Post post, CancellationToken token = default);
    Task<Post?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Post>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(Post post, CancellationToken token = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
}
