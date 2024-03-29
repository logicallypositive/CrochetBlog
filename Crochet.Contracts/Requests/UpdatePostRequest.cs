namespace Crochet.Contracts.Requests;

public class UpdatePostRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required IEnumerable<string> Category { get; init; } = Enumerable.Empty<string>();
    public required int Rating { get; init; }
    public required string ImageUrl { get; init; }
}
