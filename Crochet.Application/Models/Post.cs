namespace Crochet.Application.Models
{
    public class Post
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Rating { get; set; }
        public required List<string> Category { get; init; } = new ();
        public required DateTime DateAdded { get; set; }
    }
}
