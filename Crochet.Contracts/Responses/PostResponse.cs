namespace Crochet.Contracts.Responses
{
    public class PostResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required int Rating { get; init; }
        public required DateTime DateAdded { get; init; }
        public required IEnumerable<string> Category { get; init; } = Enumerable.Empty<string>();
    }
}
