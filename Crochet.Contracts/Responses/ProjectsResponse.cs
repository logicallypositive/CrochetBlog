namespace Crochet.Contracts.Responses
{
    public class ProjectsResponse
    {
        public required IEnumerable<ProjectResponse> Items { get; set; } = Enumerable.Empty<ProjectResponse>();
    }
}

