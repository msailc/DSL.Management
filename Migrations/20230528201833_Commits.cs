using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSLManagement.Migrations
{
    /// <inheritdoc />
    public partial class Commits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommitTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PipelineExecutionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitTitles_PipelineExecutions_PipelineExecutionId",
                        column: x => x.PipelineExecutionId,
                        principalTable: "PipelineExecutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitTitles_PipelineExecutionId",
                table: "CommitTitles",
                column: "PipelineExecutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommitTitles");
        }
    }
}
