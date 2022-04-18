using ExampleWebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleWebAPI.Data;

public class AppDBContext : DbContext
{
    // This represents a table in Entity Framework.
    public DbSet<Post> Posts => Set<Post>();

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base version of this method, else we get an error later on.
        base.OnModelCreating(modelBuilder);

        // The code below adds some example data to our database.
        Post[] postsToSeed = new Post[6];

        for (int i = 1; i < 7; i++)
        {
            postsToSeed[i - 1] = new Post
            {
                Id = i,
                Title = $"Post {i}",
                Content = $"Seeded content for post {i}.",
                Published = true
            };
        }

        modelBuilder.Entity<Post>().HasData(postsToSeed);
    }
}
