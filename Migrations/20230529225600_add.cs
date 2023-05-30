using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSLManagement.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "CommitTitles",
                newName: "CommitUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommitUrl",
                table: "CommitTitles",
                newName: "Link");
        }
    }
}
