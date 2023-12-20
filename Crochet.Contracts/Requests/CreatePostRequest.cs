namespace Crochet.Contracts.Requests
{
    public class CreatePostRequest
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required IEnumerable<string> Category { get; init; } = Enumerable.Empty<string>();
        public required int Rating { get; init; }
    }
}
