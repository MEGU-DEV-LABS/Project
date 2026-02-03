using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Teacher2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Teachers_TeacherModelId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_TeacherModelId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TeacherModelId",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ymQF8YCzbrAA8R6sMzyTBOpqinnTKoV.uYDwL6/Cq262i4PyFcRwW");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AppUsers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AppUsers_AppUserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "TeacherModelId",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "TeacherModelId" },
                values: new object[] { "$2a$11$A7U9aI.Fw3LTLNwUWN7YMupmio/QLURgwq0j73Pz3YjRTXLIDorWi", null });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TeacherModelId",
                table: "AppUsers",
                column: "TeacherModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Teachers_TeacherModelId",
                table: "AppUsers",
                column: "TeacherModelId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
