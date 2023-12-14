using Crochet.Application.Models;

namespace Crochet.Application.Repositories
{
    public interface IProjectRepository
    {

        Task<bool> CreateAsync(Project project);
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
