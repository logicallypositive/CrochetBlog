using Crochet.Application.Models;
using Crochet.Contracts.Requests;
using Crochet.Contracts.Responses;

namespace Crochet.Api.Mapping
{
    public static class ContractMapping
    {
        public static Project MapToProject(this CreateProjectRequest request)
        {
            return new Project
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Rating = request.Rating,
                Description = request.Description,
                Category = request.Category.ToList()
            };
        }

        public static ProjectResponse MapToResponse(this Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                Title = project.Title,
                Rating = project.Rating,
                Description = project.Description,
                Category = project.Category
            };
        }
        public static ProjectsResponse MapToResponse(this IEnumerable<Project> projects)
        {
            return new ProjectsResponse
            {
                Items = projects.Select(MapToResponse)
            };
        }
        public static Project MapToProject(this UpdateProjectRequest request, Guid id)
        {
            return new Project
            {
                Id = id,
                Title = request.Title,
                Rating = request.Rating,
                Description = request.Description,
                Category = request.Category.ToList()
            };
        }
    }
}
