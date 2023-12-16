namespace Crochet.Contracts.Responses
{
    public class PostsResponse
    {
        public required IEnumerable<PostResponse> Items { get; set; } = Enumerable.Empty<PostResponse>();
    }
}

