using Microsoft.EntityFrameworkCore.Migrations;

namespace HOTEL1.Data.Migrations
{
    public partial class CustomerHoteReationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerHotel",
                columns: table => new
                {
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    HotelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerHotel", x => new { x.CustomersId, x.HotelsId });
                    table.ForeignKey(
                        name: "FK_CustomerHotel_Customer_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerHotel_Hotel_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerofHotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerofHotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerofHotel_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerofHotel_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerHotel_HotelsId",
                table: "CustomerHotel",
                column: "HotelsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerofHotel_CustomerId",
                table: "CustomerofHotel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerofHotel_HotelId",
                table: "CustomerofHotel",
                column: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerHotel");

            migrationBuilder.DropTable(
                name: "CustomerofHotel");
        }
    }
}
