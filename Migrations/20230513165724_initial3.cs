using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSLManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PipelineExecutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PipelineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    Output = table.Column<string>(type: "TEXT", nullable: true),
                    Error = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineExecutions_Pipelines_PipelineId",
                        column: x => x.PipelineId,
                        principalTable: "Pipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipelineStepExecution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PipelineStepId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    ErrorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    PipelineExecutionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineStepExecution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineStepExecution_PipelineExecutions_PipelineExecutionId",
                        column: x => x.PipelineExecutionId,
                        principalTable: "PipelineExecutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PipelineStepExecution_PipelineSteps_PipelineStepId",
                        column: x => x.PipelineStepId,
                        principalTable: "PipelineSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PipelineExecutions_PipelineId",
                table: "PipelineExecutions",
                column: "PipelineId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineStepExecution_PipelineExecutionId",
                table: "PipelineStepExecution",
                column: "PipelineExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineStepExecution_PipelineStepId",
                table: "PipelineStepExecution",
                column: "PipelineStepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PipelineStepExecution");

            migrationBuilder.DropTable(
                name: "PipelineExecutions");
        }
    }
}
