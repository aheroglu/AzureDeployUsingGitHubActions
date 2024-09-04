using Microsoft.EntityFrameworkCore;
using Todos.WebAPI.Context;
using Todos.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    });

builder
    .Services
    .AddSwaggerGen();

builder
    .Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getall", (AppDbContext context) => Results.Ok(context.Todos.ToList()));

app.MapGet("/create", (string work, AppDbContext context) =>
{
    Todo todo = new()
    {
        Work = work
    };

    context.Todos.Add(todo);
    context.SaveChanges();
    Results.Ok(todo);
});

using (var scope = app.Services.CreateScope())
{
    var context = scope
        .ServiceProvider
        .GetRequiredService<AppDbContext>();

    context.Database.Migrate();
}

app.Run();
