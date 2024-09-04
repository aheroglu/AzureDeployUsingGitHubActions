using Microsoft.EntityFrameworkCore;
using Todos.WebAPI.Models;

namespace Todos.WebAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}
