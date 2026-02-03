using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class SubjectSSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Blf/.8wCxwnkavtbtsWZfeiw7Siz.eUBArlFEHHhwuFd5ku3xkz.C");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSubjects_SubjectId",
                table: "SessionSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSubjects_Subjects_SubjectId",
                table: "SessionSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSubjects_Subjects_SubjectId",
                table: "SessionSubjects");

            migrationBuilder.DropIndex(
                name: "IX_SessionSubjects_SubjectId",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "GradeECTS",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "GradeNational",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SessionSubjects");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$3q63vUcQQTrPrceXqmLHx.ZVzyJ9VOVy3SYbxzuLk27NNdKLus65y");
        }
    }
}
