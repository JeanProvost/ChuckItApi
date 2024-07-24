using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ChuckItApi.Migrations
{
    public partial class UpdateUserIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Listings",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "string");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
