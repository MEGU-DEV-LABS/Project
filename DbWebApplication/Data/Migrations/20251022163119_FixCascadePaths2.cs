using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Students_StudentModelId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "SessionSubjectsSpecialtyModel");

            migrationBuilder.DropTable(
                name: "SpecialtyModelSubjectModel");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudentModelId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudentModelId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "QrCodeToken",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TokenDateExpired",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "SessionSubjects");

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "SessionSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "QrCodeToken",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenDateExpired",
                table: "AppUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    SemesterNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    SemesterNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlans_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlanSubjectModel",
                columns: table => new
                {
                    StudyPlansId = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlanSubjectModel", x => new { x.StudyPlansId, x.SubjectsSubjectID });
                    table.ForeignKey(
                        name: "FK_StudyPlanSubjectModel_StudyPlans_StudyPlansId",
                        column: x => x.StudyPlansId,
                        principalTable: "StudyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyPlanSubjectModel_Subjects_SubjectsSubjectID",
                        column: x => x.SubjectsSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectsGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudyPlanId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    SubjectModelSubjectID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectsGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsGrades_StudyPlans_StudyPlanId",
                        column: x => x.StudyPlanId,
                        principalTable: "StudyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectsGrades_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectsGrades_Subjects_SubjectModelSubjectID",
                        column: x => x.SubjectModelSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "QrCodeToken", "TokenDateExpired" },
                values: new object[] { "$2a$11$IaW5My2LWrxjZNpguM742.2d0gw6eUB49cRMbhcTKL89zo1IQIf86", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSubjects_SessionId",
                table: "SessionSubjects",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SpecialtyId",
                table: "Sessions",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_SpecialtyId",
                table: "StudyPlans",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlanSubjectModel_SubjectsSubjectID",
                table: "StudyPlanSubjectModel",
                column: "SubjectsSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsGrades_StudentId",
                table: "SubjectsGrades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsGrades_StudyPlanId",
                table: "SubjectsGrades",
                column: "StudyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsGrades_SubjectId",
                table: "SubjectsGrades",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsGrades_SubjectModelSubjectID",
                table: "SubjectsGrades",
                column: "SubjectModelSubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSubjects_Sessions_SessionId",
                table: "SessionSubjects",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSubjects_Sessions_SessionId",
                table: "SessionSubjects");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "StudyPlanSubjectModel");

            migrationBuilder.DropTable(
                name: "SubjectsGrades");

            migrationBuilder.DropTable(
                name: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_SessionSubjects_SessionId",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeECTS",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GradeNational",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SessionSubjects");

            migrationBuilder.DropColumn(
                name: "QrCodeToken",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TokenDateExpired",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "StudentModelId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QrCodeToken",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenDateExpired",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "SessionSubjects",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "SessionSubjectsSpecialtyModel",
                columns: table => new
                {
                    SessionSubjectsId = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSubjectsSpecialtyModel", x => new { x.SessionSubjectsId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_SessionSubjectsSpecialtyModel_SessionSubjects_SessionSubjectsId",
                        column: x => x.SessionSubjectsId,
                        principalTable: "SessionSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSubjectsSpecialtyModel_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialtyModelSubjectModel",
                columns: table => new
                {
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtyModelSubjectModel", x => new { x.SpecialtiesId, x.SubjectsSubjectID });
                    table.ForeignKey(
                        name: "FK_SpecialtyModelSubjectModel_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialtyModelSubjectModel_Subjects_SubjectsSubjectID",
                        column: x => x.SubjectsSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$FUFiZlbGihZVrEt1Xt/g5uzA9iyWoxY/YokpNNniD4zdaFBlMBlOe");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentModelId",
                table: "Subjects",
                column: "StudentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSubjectsSpecialtyModel_SpecialtiesId",
                table: "SessionSubjectsSpecialtyModel",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtyModelSubjectModel_SubjectsSubjectID",
                table: "SpecialtyModelSubjectModel",
                column: "SubjectsSubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Students_StudentModelId",
                table: "Subjects",
                column: "StudentModelId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
