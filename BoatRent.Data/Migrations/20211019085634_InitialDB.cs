using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoatRent.Data.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boats",
                columns: table => new
                {
                    BoatNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BoatType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boats", x => x.BoatNumber);
                });

            migrationBuilder.CreateTable(
                name: "RentBoat",
                columns: table => new
                {
                    BookingNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoatNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentBoat", x => x.BookingNumber);
                    table.ForeignKey(
                        name: "FK_RentBoat_Boats_BoatNumber",
                        column: x => x.BoatNumber,
                        principalTable: "Boats",
                        principalColumn: "BoatNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentBoat_BoatNumber",
                table: "RentBoat",
                column: "BoatNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentBoat");

            migrationBuilder.DropTable(
                name: "Boats");
        }
    }
}
