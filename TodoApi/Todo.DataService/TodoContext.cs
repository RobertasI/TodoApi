using Microsoft.EntityFrameworkCore;
using Todo.Domain;

namespace Todo.DataService
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Domain.Todo> Todo { get; set; }

        public DbSet<TodoList> TodoList { get; set; }

        public DbSet<User> User { get; set; }
    }
}
