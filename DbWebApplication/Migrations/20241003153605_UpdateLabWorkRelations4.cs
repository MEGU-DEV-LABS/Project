using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLabWorkRelations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "LabWorks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "LabWorks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Subjects_SubjectID",
                table: "LabWorks",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
