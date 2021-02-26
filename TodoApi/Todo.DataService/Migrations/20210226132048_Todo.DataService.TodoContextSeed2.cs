using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.DataService.Migrations
{
    public partial class TodoDataServiceTodoContextSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "TodoId", "IsDone", "Text", "TodoListId" },
                values: new object[,]
                {
                    { 1L, false, "Make a sandwich", 1L },
                    { 2L, false, "Do a pullup", 1L },
                    { 3L, true, "Make an api", 1L }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Password", "UserRole" },
                values: new object[] { 3L, "belekas@belekas.com", "passwordas2355189", 2 });

            migrationBuilder.InsertData(
                table: "TodoList",
                columns: new[] { "TodoListId", "UserId" },
                values: new object[] { 2L, 3L });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "TodoId", "IsDone", "Text", "TodoListId" },
                values: new object[] { 4L, false, "Make 2 sandwiches", 2L });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "TodoId", "IsDone", "Text", "TodoListId" },
                values: new object[] { 5L, false, "Do 2 pullups", 2L });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "TodoId", "IsDone", "Text", "TodoListId" },
                values: new object[] { 6L, true, "Make 2 apies", 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "TodoId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "TodoList",
                keyColumn: "TodoListId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3L);
        }
    }
}
