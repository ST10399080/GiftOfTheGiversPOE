using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGiversPOE.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectUpdates",
                columns: table => new
                {
                    ProjectUpdateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReliefProjectID = table.Column<int>(type: "int", nullable: false),
                    UpdateText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUpdates", x => x.ProjectUpdateID);
                    table.ForeignKey(
                        name: "FK_ProjectUpdates_ReliefProjects_ReliefProjectID",
                        column: x => x.ReliefProjectID,
                        principalTable: "ReliefProjects",
                        principalColumn: "ReliefProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_ReliefProjectID",
                table: "ProjectUpdates",
                column: "ReliefProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUpdates");
        }
    }
}
