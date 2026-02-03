using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class GradeStudyPlanRemovale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsGrades_StudyPlans_StudyPlanId",
                table: "SubjectsGrades");

            migrationBuilder.DropIndex(
                name: "IX_SubjectsGrades_StudyPlanId",
                table: "SubjectsGrades");

            migrationBuilder.DropColumn(
                name: "StudyPlanId",
                table: "SubjectsGrades");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$mMbMzhfgYF.RbVqnlGwgRuvJZCEvqYTxjfyra6NGmEXeT6wJe9h2G");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyPlanId",
                table: "SubjectsGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$fapVsiSTXT9nLUhzyC.qMuMtMI3g4jwwAf9EmopDrZOsFjyjyBFBi");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsGrades_StudyPlanId",
                table: "SubjectsGrades",
                column: "StudyPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsGrades_StudyPlans_StudyPlanId",
                table: "SubjectsGrades",
                column: "StudyPlanId",
                principalTable: "StudyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
