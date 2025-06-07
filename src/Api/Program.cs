using Api.Middleware;
using Application.Core;
using Application.Validators;
using Application.VideoGames;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Yordan'sCorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:3000", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetVideoGameList.Handler>();
    cfg.AddOpenBehavior(typeof(ValidationMechanism<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateVideoGameValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Yordan'sCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
}

app.Run();
