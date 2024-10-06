using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLabWorkRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Subjects_SubjectModelSubjectID",
                table: "LabWorks");

            migrationBuilder.DropIndex(
                name: "IX_LabWorks_SubjectModelSubjectID",
                table: "LabWorks");

            migrationBuilder.DropColumn(
                name: "SubjectModelSubjectID",
                table: "LabWorks");

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "LabWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabWorks_SubjectID",
                table: "LabWorks",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks");

            migrationBuilder.DropIndex(
                name: "IX_LabWorks_SubjectID",
                table: "LabWorks");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "LabWorks");

            migrationBuilder.AddColumn<int>(
                name: "SubjectModelSubjectID",
                table: "LabWorks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabWorks_SubjectModelSubjectID",
                table: "LabWorks",
                column: "SubjectModelSubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Subjects_SubjectModelSubjectID",
                table: "LabWorks",
                column: "SubjectModelSubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }
    }
}
