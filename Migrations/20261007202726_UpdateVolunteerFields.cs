using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGiversPOE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVolunteerFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Volunteers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Volunteers",
                newName: "GeographicFlexibility");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Volunteers",
                newName: "AdditionalInformation");

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Volunteers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Volunteers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Volunteers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Volunteers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "GeographicFlexibility",
                table: "Volunteers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "Volunteers",
                newName: "Experience");

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "Volunteers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
