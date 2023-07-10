using Microsoft.EntityFrameworkCore.Migrations;

namespace eBoxOffice.Repository.Migrations
{
    public partial class ticketInOrderaddedquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TicketsInOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TicketsInOrders");
        }
    }
}
