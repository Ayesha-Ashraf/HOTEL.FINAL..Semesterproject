using Microsoft.EntityFrameworkCore.Migrations;

namespace HOTEL1.Data.Migrations
{
    public partial class HoteRoomReation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_RoomId",
                table: "Hotel",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Room_RoomId",
                table: "Hotel",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Room_RoomId",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_RoomId",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Hotel");
        }
    }
}
