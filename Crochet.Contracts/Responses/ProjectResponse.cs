namespace Crochet.Contracts.Responses
{
    public class ProjectResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required IEnumerable<string> Category { get; init; } = Enumerable.Empty<string>();
        public required int Rating { get; init; }
    }

}

