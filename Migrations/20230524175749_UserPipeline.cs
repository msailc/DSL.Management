using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSLManagement.Migrations
{
    /// <inheritdoc />
    public partial class UserPipeline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pipelines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_UserId",
                table: "Pipelines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pipelines_Users_UserId",
                table: "Pipelines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pipelines_Users_UserId",
                table: "Pipelines");

            migrationBuilder.DropIndex(
                name: "IX_Pipelines_UserId",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pipelines");
        }
    }
}
