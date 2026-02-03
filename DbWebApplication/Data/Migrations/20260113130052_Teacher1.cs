using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Teacher1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_Specialties_SpecialtyId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SessionSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherModelId",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "TeacherId", "TeacherModelId" },
                values: new object[] { "$2a$11$A7U9aI.Fw3LTLNwUWN7YMupmio/QLURgwq0j73Pz3YjRTXLIDorWi", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSubjects_TeacherId",
                table: "SessionSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TeacherModelId",
                table: "AppUsers",
                column: "TeacherModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FacultyId",
                table: "Teachers",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Teachers_TeacherModelId",
                table: "AppUsers",
                column: "TeacherModelId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSubjects_Teachers_TeacherId",
                table: "SessionSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_Specialties_SpecialtyId",
                table: "StudyPlans",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Teachers_TeacherModelId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSubjects_Teachers_TeacherId",
                table: "SessionSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_Specialties_SpecialtyId",
                table: "StudyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_SessionSubjects_TeacherId",
                table: "SessionSubjects");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_TeacherModelId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TeacherModelId",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IaW5My2LWrxjZNpguM742.2d0gw6eUB49cRMbhcTKL89zo1IQIf86");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_Specialties_SpecialtyId",
                table: "StudyPlans",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
