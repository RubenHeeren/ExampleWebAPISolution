using ExampleWebAPI.Data;
using ExampleWebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Set Swagger to run on the root route. No /swagger needed.    
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API facilitating CRUD operations on the Posts table.");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

// CREATE a post.
app.MapPost("/posts", async (PostCreateUpdateDTO postToCreateDTO, AppDBContext dbContext) =>
{
    Post postToCreate = new()
    {
        // Let Entity Framework Core automatically increment the ID.
        Id = 0,
        Title = postToCreateDTO.Title,
        Content = postToCreateDTO.Content,
        Published = postToCreateDTO.Published
    };

    dbContext.Posts.Add(postToCreate);

    bool changesPersistedToDB = await dbContext.SaveChangesAsync() > 0;

    if (changesPersistedToDB)
    {
        // Returns the route of the created object.
        return Results.Created($"/posts/{postToCreate.Id}", postToCreate);
    }
    else
    {
        return Results.BadRequest();
    }
});

// GET endpoint mapped to /posts. This method is asynchronous and uses dependency injection to get the AppDBContext dependency.
// READ all posts.
app.MapGet("/posts", async (AppDBContext dbContext) =>
{
    List<Post> allPosts = await dbContext.Posts.ToListAsync();
    return allPosts;
});

// READ a single post by id.
app.MapGet("/posts/{postId}", async (int postId, AppDBContext dbContext) =>
{
    Post? post = await dbContext.Posts.FindAsync(postId);

    if (post != null)
    {
        return Results.Ok(post);
    }
    else
    {
        return Results.NotFound();
    }
});

// UPDATE a post.
app.MapPut("/posts/{postId}", async (int postId, PostCreateUpdateDTO updatedPostDTO, AppDBContext dbContext) =>
{
    var postToUpdate = await dbContext.Posts.FindAsync(postId);

    if (postToUpdate == null)
    {
        return Results.NotFound();
    }

    postToUpdate.Title = updatedPostDTO.Title;
    postToUpdate.Content = updatedPostDTO.Content;
    postToUpdate.Published = updatedPostDTO.Published;

    bool changesPersistedToDB = await dbContext.SaveChangesAsync() > 0;

    if (changesPersistedToDB)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.BadRequest();
    }
});

// DELETE a post.
app.MapDelete("/posts/{postId}", async (int postId, AppDBContext dbContext) =>
{
    Post? postToDelete = await dbContext.Posts.FindAsync(postId);

    if (postToDelete != null)
    {
        dbContext.Posts.Remove(postToDelete);

        bool changesPersistedToDB = await dbContext.SaveChangesAsync() > 0;

        if (changesPersistedToDB)
        {
            return Results.NoContent();
        }
        else
        {
            return Results.BadRequest();
        }
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();
