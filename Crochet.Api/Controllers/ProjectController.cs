using Microsoft.AspNetCore.Mvc;
using Crochet.Application.Repositories;
using Crochet.Contracts.Requests;
using Crochet.Api.Mapping;

namespace Crochet.Api.Controllers
{
    [ApiController]
    public class ProjectController(IProjectRepository projectRepository) : ControllerBase
    {
        [HttpPost(ApiEndpoints.Projects.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var project = request.MapToProject();
            await projectRepository.CreateAsync(project);
            return Created($"/{ApiEndpoints.Projects.Create}{project.Id}", project);
        }

        [HttpGet(ApiEndpoints.Projects.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var project = await projectRepository.GetByIdAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            var response = project.MapToResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Projects.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var projects = await projectRepository.GetAllAsync();

            var projectsResponse = projects.MapToResponse();

            return Ok(projectsResponse);
        }

        [HttpPut(ApiEndpoints.Projects.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateProjectRequest request)
        {
            var project = request.MapToProject(id);
            var updated = await projectRepository.UpdateAsync(project);

            if (!updated)
            {
                return NotFound();
            }

            var response = project.MapToResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Projects.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await projectRepository.DeleteByIdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

