using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentSubjectRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_StudentID",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectID",
                table: "StudentSubject");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "StudentSubject",
                newName: "SubjectsSubjectID");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "StudentSubject",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_SubjectID",
                table: "StudentSubject",
                newName: "IX_StudentSubject_SubjectsSubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_StudentsId",
                table: "StudentSubject",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectsSubjectID",
                table: "StudentSubject",
                column: "SubjectsSubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_StudentsId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectsSubjectID",
                table: "StudentSubject");

            migrationBuilder.RenameColumn(
                name: "SubjectsSubjectID",
                table: "StudentSubject",
                newName: "SubjectID");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentSubject",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_SubjectsSubjectID",
                table: "StudentSubject",
                newName: "IX_StudentSubject_SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_StudentID",
                table: "StudentSubject",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectID",
                table: "StudentSubject",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
