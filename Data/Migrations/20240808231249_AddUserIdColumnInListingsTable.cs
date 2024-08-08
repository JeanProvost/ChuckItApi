using Microsoft.EntityFrameworkCore.Migrations;

namespace ChuckItApi.Migrations
{
    public partial class AddUserIdColumnInListingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Listings",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Listings");
        }
    }
}
