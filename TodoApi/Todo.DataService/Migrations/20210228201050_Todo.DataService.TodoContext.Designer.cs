﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.DataService;

namespace Todo.DataService.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20210228201050_Todo.DataService.TodoContext")]
    partial class TodoDataServiceTodoContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Todo.Domain.Todo", b =>
                {
                    b.Property<long>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("TodoListId")
                        .HasColumnType("bigint");

                    b.HasKey("TodoId");

                    b.HasIndex("TodoListId");

                    b.ToTable("Todo");

                    b.HasData(
                        new
                        {
                            TodoId = 1L,
                            IsDone = false,
                            Text = "Make a sandwich",
                            TodoListId = 1L
                        },
                        new
                        {
                            TodoId = 2L,
                            IsDone = false,
                            Text = "Do a pullup",
                            TodoListId = 1L
                        },
                        new
                        {
                            TodoId = 3L,
                            IsDone = true,
                            Text = "Make an api",
                            TodoListId = 1L
                        },
                        new
                        {
                            TodoId = 4L,
                            IsDone = false,
                            Text = "Make 2 sandwiches",
                            TodoListId = 2L
                        },
                        new
                        {
                            TodoId = 5L,
                            IsDone = false,
                            Text = "Do 2 pullups",
                            TodoListId = 2L
                        },
                        new
                        {
                            TodoId = 6L,
                            IsDone = true,
                            Text = "Make 2 apies",
                            TodoListId = 2L
                        });
                });

            modelBuilder.Entity("Todo.Domain.TodoList", b =>
                {
                    b.Property<long>("TodoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("TodoListId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TodoList");

                    b.HasData(
                        new
                        {
                            TodoListId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            TodoListId = 2L,
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("Todo.Domain.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            Email = "admin@admin.com",
                            Password = "superadmin3000",
                            UserRole = 1
                        },
                        new
                        {
                            UserId = 2L,
                            Email = "ilginis.robertas@gmail.com",
                            Password = "slaptazodis12345",
                            UserRole = 2
                        },
                        new
                        {
                            UserId = 3L,
                            Email = "belekas@belekas.com",
                            Password = "passwordas2355189",
                            UserRole = 2
                        });
                });

            modelBuilder.Entity("Todo.Domain.Todo", b =>
                {
                    b.HasOne("Todo.Domain.TodoList", "TodoList")
                        .WithMany("Todos")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Todo.Domain.TodoList", b =>
                {
                    b.HasOne("Todo.Domain.User", "User")
                        .WithOne("TodoList")
                        .HasForeignKey("Todo.Domain.TodoList", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
