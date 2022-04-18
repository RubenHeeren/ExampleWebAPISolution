using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleWebAPI.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 1, "Seeded content for post 1.", true, "Post 1" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 2, "Seeded content for post 2.", true, "Post 2" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 3, "Seeded content for post 3.", true, "Post 3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 4, "Seeded content for post 4.", true, "Post 4" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 5, "Seeded content for post 5.", true, "Post 5" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Published", "Title" },
                values: new object[] { 6, "Seeded content for post 6.", true, "Post 6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
