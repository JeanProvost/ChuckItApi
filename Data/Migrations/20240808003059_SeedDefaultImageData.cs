using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ChuckItApi.Migrations
{
    public partial class SeedDefaultImageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "FileName", "ListingId" },
                values: new object[,]
                {
                    {
                        1,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/jacket1.jpg",
                        Guid.Parse("819a1802-dc2f-496d-9fd7-d9dc2cfbe13d"),
                    },
                    {
                        2,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/couch2.png",
                        Guid.Parse("a4eda163-371b-4e90-b911-925e5e2f1bd8"),
                    },
                    {
                        3,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/couch2.png",
                        Guid.Parse("18935577-6fa0-4a3c-907d-68440e5cde28"),
                    },
                    {
                        4,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/shoes1.png",
                        Guid.Parse("41d779dc-cf5a-424d-870c-e3d48ba11b10"),
                    },
                    {
                        5,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/camera1.jpg",
                        Guid.Parse("2270b73e-188b-4bc1-b421-4dda679cf0cf"),
                    },
                    {
                        6,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/camera2.png",
                        Guid.Parse("2bce2398-fde9-43b9-99ab-2859e92113f2"),
                    },
                    {
                        7,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/couch3.png",
                        Guid.Parse("f145a95d-51ac-4a6e-b747-44d2f1d9100f"),
                    },
                    {
                        8,
                        "https://chuckit-development.s3.ca-central-1.amazonaws.com/shoes2.png",
                        Guid.Parse("1aeaed0f-2109-42ec-af65-1ee39a9a70b4")
                    }

                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                });
        }
    }
}
