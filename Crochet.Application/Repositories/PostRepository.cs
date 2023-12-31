using Crochet.Application.Database;
using Crochet.Application.Models;
using Dapper;

namespace Crochet.Application.Repositories;

public class PostRepository(IDbConnectionFactory dbConnectionFactory) : IPostRepository
{
    public async Task<bool> CreateAsync(Post post)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                INSERT INTO posts (id, title, description, rating, date_added)
                VALUES (@Id, @Title, @Description, @Rating, @DateAdded)
                """;

        int result = await connection.ExecuteAsync(new CommandDefinition(sql1, post));

        if (result > 0)
        {
            foreach (var category in post.Category)
            {
                var sql2 = """
                        INSERT INTO categories (postId, name)
                        VALUES (@PostId, @Name)
                        """;
                await connection.ExecuteAsync(
                    new CommandDefinition(sql2, new { PostId = post.Id, Name = category })
                );
            }
        }
        transaction.Commit();

        return result > 0;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var sql1 = """
                SELECT id, title, description, rating, date_added AS dateAdded
                FROM posts
                WHERE id = @id
                """;

        var post = await connection.QuerySingleOrDefaultAsync<Post>(
            new CommandDefinition(sql1, new { id })
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
            new CommandDefinition(sql2, new { id })
        );

        foreach (var category in categories)
        {
            post.Category.Add(category);
        }

        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();

        var sql1 = """
                SELECT p.*, string_agg(c.name, ',') AS categories
                FROM posts p
                LEFT JOIN categories c ON p.id = c.postid
                GROUP BY id
                """;
        var result = await connection.QueryAsync(new CommandDefinition(sql1));

        return result.Select(
            x =>
                new Post
                {
                    Id = x.id,
                    Title = x.title,
                    Description = x.description,
                    Rating = x.rating,
                    DateAdded = x.date_added,
                    Category = Enumerable.ToList(x.categories.Split(','))
                }
        );
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                DELETE FROM categories 
                WHERE postid = @id
                """;
        await connection.ExecuteAsync(new CommandDefinition(sql1, new { id = post.Id }));

        foreach (var category in post.Category)
        {
            var sql2 = """
                    INSERT INTO categories (postId, name)
                    VALUES (@PostId, @Name)
                    """;
            await connection.ExecuteAsync(
                new CommandDefinition(sql2, new { PostId = post.Id, Name = category })
            );
        }

        var sql3 = """
                UPDATE posts
                SET title=@Title, description=@Description, rating=@Rating, date_added=@DateAdded
                WHERE id = @Id
                """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql3, post));

        transaction.Commit();

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql1 = """
                DELETE FROM categories
                WHERE postid = @id
                """;
        await connection.ExecuteAsync(new CommandDefinition(sql1, new { id }));

        var sql2 = """
                DELETE FROM posts
                WHERE id = @id
                """;
        var result = await connection.ExecuteAsync(new CommandDefinition(sql2, new { id }));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var sql1 = """
                SELECT COUNT(1) 
                FROM posts
                WHERE id = @id
                """;
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql1, new { id }));
    }
}
