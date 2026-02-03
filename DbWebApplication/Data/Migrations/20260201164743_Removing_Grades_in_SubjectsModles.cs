using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Removing_Grades_in_SubjectsModles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeECTS",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeNational",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeECTS",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "GradeNational",
                table: "SessionSubjects");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$btcV3yOq/QQR2lahoZ7MBuyqroCcgCgP8Dc9D8nyCR.TDWNlKZZ2G");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradeECTS",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeNational",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeECTS",
                table: "SessionSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeNational",
                table: "SessionSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$tR8HzEterhgfy2x/ffjMses86sh2kncWKp25GaZo4mVR4oEnUNVFm");
        }
    }
}
