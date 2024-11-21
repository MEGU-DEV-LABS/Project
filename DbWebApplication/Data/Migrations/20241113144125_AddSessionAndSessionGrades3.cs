using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAndSessionGrades3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Faculty",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Faculty",
                table: "SessionSubjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
