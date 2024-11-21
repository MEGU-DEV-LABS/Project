using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAndSessionGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<float>(type: "real", nullable: false),
                    SessionSubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionGrades_SessionSubjects_SessionSubjectsId",
                        column: x => x.SessionSubjectsId,
                        principalTable: "SessionSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionGrades_SessionSubjectsId",
                table: "SessionGrades",
                column: "SessionSubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionGrades_StudentId",
                table: "SessionGrades",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionGrades");

            migrationBuilder.DropTable(
                name: "SessionSubjects");
        }
    }
}
