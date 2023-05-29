using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSLManagement.Migrations
{
    /// <inheritdoc />
    public partial class userid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PipelineExecutions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PipelineExecutions_UserId",
                table: "PipelineExecutions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PipelineExecutions_Users_UserId",
                table: "PipelineExecutions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PipelineExecutions_Users_UserId",
                table: "PipelineExecutions");

            migrationBuilder.DropIndex(
                name: "IX_PipelineExecutions_UserId",
                table: "PipelineExecutions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PipelineExecutions");
        }
    }
}
