using System;
using ChuckItApi.Models;
using Microsoft.EntityFrameworkCore.Migrations;


namespace ChuckItApi.Migrations
{
    public partial class SeedListingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Listings",
            columns: new[] { "Id", "Title", "Description", "Price", "CategoryId", "LocationId", "UserId" },
            values: new object[,]
            {
                {
                    Guid.NewGuid(),
                    "Red Jacket",
                    "",
                    100,
                    5,
                    1,
                    Guid.Parse("71fa4b95-079d-42b1-a2cb-dd26028aed9d"),
                },
                {
                    Guid.NewGuid(),
                    "Gray couch in a great condition",
                    "",
                    1200,
                    1,
                    1,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                    Guid.NewGuid(),
                    "Room & Board couch (great condition) - delivery included",
                    "I'm selling my furniture at a discount price. Pick up at Venice. DM me asap.",
                    1000,
                    1,
                    2,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                     Guid.NewGuid(),
                     "Designer wear shoes",
                     "",
                     100,
                     5,
                     2,
                     Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                    Guid.NewGuid(),
                    "Canon 400D (Great Condition)",
                    "",
                    300,
                    3,
                    3,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                    Guid.NewGuid(),
                    "Nikon D850 for sale",
                    "",
                    250,
                    3,
                    3,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                    Guid.NewGuid(),
                    "Sectional couch - Delivery available",
                    "No rips no stains no odors",
                    250,
                    1,
                    4,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                },
                {
                    Guid.NewGuid(),
                    "Brown leather shoes",
                    "No rips no stains no odors",
                    250,
                    5,
                    5,
                    Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
