using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGiversPOE.Migrations
{
    /// <inheritdoc />
    public partial class AddDonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DonorEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DonationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReliefProjectID = table.Column<int>(type: "int", nullable: true),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaxCertificateNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationID);
                    table.ForeignKey(
                        name: "FK_Donations_ReliefProjects_ReliefProjectID",
                        column: x => x.ReliefProjectID,
                        principalTable: "ReliefProjects",
                        principalColumn: "ReliefProjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ReliefProjectID",
                table: "Donations",
                column: "ReliefProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");
        }
    }
}
