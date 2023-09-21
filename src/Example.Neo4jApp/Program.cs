using Neo4j.Driver;

namespace Example.Neo4jApp
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			// Assumes a local neo4j DB exposed on port 7687, user "neo4j", no password
			var driver = GraphDatabase.Driver(
				"bolt://localhost:7687/",
				AuthTokens.Basic("neo4j", ""));

			var movies = await GetMoviesByTitle(driver, "Apollo 13");

			foreach (var movie in movies) 
			{
				Console.WriteLine($"Found movie: {movie.Title}\nWith Tagline: {movie.Tagline}\nReleased in: {movie.Release}");
			}			
        }

		private static async Task<List<Movie>> GetMoviesByTitle(IDriver driver, string movieTitle)
		{
			await using var session = driver.AsyncSession(builder => builder.WithDatabase("neo4j"));

			return await session.ExecuteReadAsync(async transaction =>
			{
				var cursor = await transaction.RunAsync(
@"
	MATCH (movie:Movie)
    WHERE toLower(movie.title) CONTAINS toLower($title)
    RETURN movie.title AS title,
            movie.released AS released,
            movie.tagline AS tagline", new { title = movieTitle });

				return await cursor.ToListAsync(record => new Movie
				{ 
					Title = record["title"].As<string>(),
					Tagline = record["tagline"].As<string>(),
					Release = record["released"].As<long>()
				});
			});
		}		
	}
}