using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PSTask_PSTask_PSTaskId",
                table: "PSTask");

            migrationBuilder.DropIndex(
                name: "IX_PSTask_PSTaskId",
                table: "PSTask");

            migrationBuilder.DropColumn(
                name: "PSTaskId",
                table: "PSTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PSTaskId",
                table: "PSTask",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PSTask_PSTaskId",
                table: "PSTask",
                column: "PSTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_PSTask_PSTask_PSTaskId",
                table: "PSTask",
                column: "PSTaskId",
                principalTable: "PSTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
