using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChuckItApi.Migrations
{
    public partial class SeedCategoriesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Category",
            columns: new[] { "Id", "Name", "Icon", "BackgroundColor", "Color" },
            values: new object[,]
            {
                { 1, "Furniture", "floor-lamp", "#fc5c65", "white" },
                { 2, "Cars", "car", "#fd9644", "white" },
                { 3, "Cameras", "camera", "#fed330", "white" },
                { 4, "Games", "cards", "#26de81", "white" },
                { 5, "Clothing", "shoe-heel", "#2bcbba", "white" },
                { 6, "Sports", "basketball", "#45aaf2", "white" },
                { 7, "Movies & Music", "headphones", "#4b7bec", "white" },
                { 8, "Books", "book-open-variant", "#a55eea", "white" },
                { 9, "Other", "application", "#778ca3", "white" }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}
