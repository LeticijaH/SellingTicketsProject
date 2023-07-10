using Microsoft.EntityFrameworkCore.Migrations;

namespace eBoxOffice.Repository.Migrations
{
    public partial class moviemodelwithcategoryanddescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                table: "Tickets",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
