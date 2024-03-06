using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH_Proyect.Server.Migrations
{
    public partial class reverseLastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_Department_DepartmentId",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_DepartmentId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Position");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Position",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Position_DepartmentId",
                table: "Position",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Department_DepartmentId",
                table: "Position",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
