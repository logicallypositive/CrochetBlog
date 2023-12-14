using Crochet.Application.Models;

namespace Crochet.Application.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = [];

        public Task<bool> CreateAsync(Project project)
        {
            _projects.Add(project);
            return Task.FromResult(true);
        }
        public Task<Project?> GetByIdAsync(Guid id)
        {
            Project? project = _projects.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(project);
        }
        public Task<IEnumerable<Project>> GetAllAsync()
        {
            return Task.FromResult(_projects.AsEnumerable());
        }

        public Task<bool> UpdateAsync(Project project)
        {
            int projectIndex = _projects.FindIndex(x => x.Id == project.Id);
            if (projectIndex == -1)
            {
                return Task.FromResult(false);
            }
            _projects[projectIndex] = project;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            int removedCount = _projects.RemoveAll(x => x.Id == id);
            bool projectRemoved = removedCount > 0;
            return Task.FromResult(projectRemoved);
        }
    }
}
