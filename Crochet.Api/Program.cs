using Crochet.Api.Mapping;
using Crochet.Application;
using Crochet.Application.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// CORS
builder.Services.AddCors(opt => 
{
    opt.AddPolicy("CorsPolicy", policy => 
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddDatabase(config["Database:ConnectionString"]!);

WebApplication app = builder.Build();


app.UseCors("CorsPolicy");
app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();
app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
