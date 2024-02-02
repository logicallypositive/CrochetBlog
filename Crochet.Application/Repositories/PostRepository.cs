using Crochet.Application.Database;
using Crochet.Application.Models;
using Dapper;

namespace Crochet.Application.Repositories;

public class PostRepository(IDbConnectionFactory dbConnectionFactory) : IPostRepository
{
    public async Task<bool> CreateAsync(Post post, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                INSERT INTO posts (id, title, description, rating, date_added, image_url)
                VALUES (@Id, @Title, @Description, @Rating, @DateAdded, @ImageUrl)
                """;

        int result = await connection.ExecuteAsync(
            new CommandDefinition(sql1, post, cancellationToken: token)
        );

        if (result > 0)
        {
            foreach (var category in post.Category)
            {
                var sql2 = """
                        INSERT INTO categories (postId, name)
                        VALUES (@PostId, @Name)
                        """;
                await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql2,
                        new { PostId = post.Id, Name = category },
                        cancellationToken: token
                    )
                );
            }
        }
        transaction.Commit();

        return result > 0;
    }

    public async Task<Post?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        var sql1 = """
                SELECT id, title, description, rating, date_added, image_url AS dateAdded
                FROM posts
                WHERE id = @id
                """;

        var post = await connection.QuerySingleOrDefaultAsync<Post>(
            new CommandDefinition(sql1, new { id }, cancellationToken: token)
        );

        if (post is null)
        {
            return null;
        }

        var sql2 = """
                SELECT (name)
                FROM categories
                WHERE postId = @id
                """;

        var categories = await connection.QueryAsync<string>(
            new CommandDefinition(sql2, new { id }, cancellationToken: token)
        );

        foreach (var category in categories)
        {
            post.Category.Add(category);
        }

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);

        var sql1 = """
                SELECT p.*, string_agg(c.name, ',') AS categories
                FROM posts p
                LEFT JOIN categories c ON p.id = c.postid
                GROUP BY id
                """;
        var result = await connection.QueryAsync(
            new CommandDefinition(sql1, cancellationToken: token)
        );

        return result.Select(
            x =>
                new Post
                {
                    Id = x.id,
                    Title = x.title,
                    Description = x.description,
                    Rating = x.rating,
                    DateAdded = x.date_added,
                    Category = Enumerable.ToList(x.categories.Split(',')),
                    ImageUrl = x.image_url
                }
        );
    }

    public async Task<bool> UpdateAsync(Post post, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                DELETE FROM categories 
                WHERE postid = @id
                """;
        await connection.ExecuteAsync(
            new CommandDefinition(sql1, new { id = post.Id }, cancellationToken: token)
        );

        foreach (var category in post.Category)
        {
            var sql2 = """
                    INSERT INTO categories (postId, name)
                    VALUES (@PostId, @Name)
                    """;
            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql2,
                    new { PostId = post.Id, Name = category },
                    cancellationToken: token
                )
            );
        }

        var sql3 = """
                UPDATE posts
                SET title=@Title, description=@Description, rating=@Rating, date_added=@DateAdded, image_url=@ImageUrl
                WHERE id = @Id
                """;

        var result = await connection.ExecuteAsync(
            new CommandDefinition(sql3, post, cancellationToken: token)
        );

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                DELETE FROM categories
                WHERE postid = @id
                """;
        await connection.ExecuteAsync(
            new CommandDefinition(sql1, new { id }, cancellationToken: token)
        );

        var sql2 = """
                DELETE FROM posts
                WHERE id = @id
                """;
        var result = await connection.ExecuteAsync(
            new CommandDefinition(sql2, new { id }, cancellationToken: token)
        );

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        var sql1 = """
                SELECT COUNT(1) 
                FROM posts
                WHERE id = @id
                """;
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql1, new { id }, cancellationToken: token)
        );
    }
}
