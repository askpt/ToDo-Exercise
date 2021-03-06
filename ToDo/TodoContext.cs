using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo
{
    [ExcludeFromCodeCoverage]
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "test", Password = "pwd123" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Username = "temp", Password = "pwd123" });
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}