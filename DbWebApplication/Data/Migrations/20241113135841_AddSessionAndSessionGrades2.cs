using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAndSessionGrades2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionGrades_SessionSubjects_SessionSubjectsId",
                table: "SessionGrades");

            migrationBuilder.DropIndex(
                name: "IX_SessionGrades_SessionSubjectsId",
                table: "SessionGrades");

            migrationBuilder.DropColumn(
                name: "SessionSubjectsId",
                table: "SessionGrades");

            migrationBuilder.CreateIndex(
                name: "IX_SessionGrades_SessionId",
                table: "SessionGrades",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionGrades_SessionSubjects_SessionId",
                table: "SessionGrades",
                column: "SessionId",
                principalTable: "SessionSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionGrades_SessionSubjects_SessionId",
                table: "SessionGrades");

            migrationBuilder.DropIndex(
                name: "IX_SessionGrades_SessionId",
                table: "SessionGrades");

            migrationBuilder.AddColumn<int>(
                name: "SessionSubjectsId",
                table: "SessionGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SessionGrades_SessionSubjectsId",
                table: "SessionGrades",
                column: "SessionSubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionGrades_SessionSubjects_SessionSubjectsId",
                table: "SessionGrades",
                column: "SessionSubjectsId",
                principalTable: "SessionSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
