using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeChallenge.Migrations
{
    /// <inheritdoc />
    public partial class Implementingnestedsetforcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lft",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rgt",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lft",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Rgt",
                table: "Categories");
        }
    }
}
