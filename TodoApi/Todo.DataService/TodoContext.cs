using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //users

            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                Password = "superadmin3000",
                Email = "admin@admin.com",
                UserRole = 1,

            });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 2,
                Password = "slaptazodis12345",
                Email = "ilginis.robertas@gmail.com",
                UserRole = 2

            });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 3,
                Password = "passwordas2355189",
                Email = "belekas@belekas.com",
                UserRole = 2,

            });

            //todo lists
            modelBuilder.Entity<TodoList>().HasData(
            new TodoList
            {
                UserId = 2,
                TodoListId = 1,

            });

            modelBuilder.Entity<TodoList>().HasData(
            new TodoList
            {
                UserId = 3,
                TodoListId = 2,

            });


            //todos
            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 1,
                TodoId = 1,
                Text = "Make a sandwich",
                IsDone = false

            });

            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 1,
                TodoId = 2,
                Text = "Do a pullup",
                IsDone = false

            });

            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 1,
                TodoId = 3,
                Text = "Make an api",
                IsDone = true

            });

            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 2,
                TodoId = 4,
                Text = "Make 2 sandwiches",
                IsDone = false

            });

            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 2,
                TodoId = 5,
                Text = "Do 2 pullups",
                IsDone = false

            });

            modelBuilder.Entity<Domain.Todo>().HasData(
            new Domain.Todo
            {
                TodoListId = 2,
                TodoId = 6,
                Text = "Make 2 apies",
                IsDone = true

            });

        }
    }
}
