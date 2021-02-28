using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.DataService.Migrations
{
    public partial class TodoDataServiceTodoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    TodoListId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoList", x => x.TodoListId);
                    table.ForeignKey(
                        name: "FK_TodoList_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    TodoId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false),
                    TodoListId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todo_TodoList_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "TodoList",
                        principalColumn: "TodoListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Password", "UserRole" },
                values: new object[] { 1L, "admin@admin.com", "superadmin3000", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Password", "UserRole" },
                values: new object[] { 2L, "ilginis.robertas@gmail.com", "slaptazodis12345", 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Password", "UserRole" },
                values: new object[] { 3L, "belekas@belekas.com", "passwordas2355189", 2 });

            migrationBuilder.InsertData(
                table: "TodoList",
                columns: new[] { "TodoListId", "UserId" },
                values: new object[] { 1L, 2L });

            migrationBuilder.InsertData(
                table: "TodoList",
                columns: new[] { "TodoListId", "UserId" },
                values: new object[] { 2L, 3L });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "TodoId", "IsDone", "Text", "TodoListId" },
                values: new object[,]
                {
                    { 1L, false, "Make a sandwich", 1L },
                    { 2L, false, "Do a pullup", 1L },
                    { 3L, true, "Make an api", 1L },
                    { 4L, false, "Make 2 sandwiches", 2L },
                    { 5L, false, "Do 2 pullups", 2L },
                    { 6L, true, "Make 2 apies", 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TodoListId",
                table: "Todo",
                column: "TodoListId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoList_UserId",
                table: "TodoList",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "TodoList");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
