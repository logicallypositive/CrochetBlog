using Dapper;

namespace Crochet.Application.Database
{
    public class DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        public async Task InitializeAsync()
        {
            using var connection = await dbConnectionFactory.CreateConnectionAsync();

            var sql1 = """
                CREATE TABLE IF NOT EXISTS posts (
                id UUID PRIMARY KEY,
                title TEXT NOT NULL,
                description TEXT NOT NULL,
                rating INTEGER NOT NULL,
                date_added DATE NOT NULL);
            """;
            await connection.ExecuteAsync(sql1);

            var sql2 = """
                CREATE TABLE IF NOT EXISTS categories (
                    postId UUID REFERENCES posts (Id),
                    name TEXT NOT NULL);
            """;
            await connection.ExecuteAsync(sql2);
        }
    }
}
