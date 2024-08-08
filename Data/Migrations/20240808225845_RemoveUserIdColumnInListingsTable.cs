using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChuckItApi.Migrations
{
    public partial class RemoveUserIdColumnInListingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Listings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
