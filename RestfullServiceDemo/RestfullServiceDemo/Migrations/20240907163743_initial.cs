using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfullServiceDemo.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "dbo",
                columns: table => new
                {
                    BookID = table.Column<int>(name: "Book ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(name: "Book Title", type: "nvarchar(max)", nullable: false),
                    BookDescription = table.Column<string>(name: "Book Description", type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quanlity = table.Column<int>(type: "int", nullable: false),
                    Delete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.BookID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "dbo");
        }
    }
}
