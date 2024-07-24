using ChuckItApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ChuckItApi.Data.Seeds
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Listings.Any())
                {
                    return;
                }

                var listings = new Listing[]
                {
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Red jacket",
                        Images = new Image[] { new Image { FileName = "jacket1" } },
                        Price = 100,
                        CategoryId = 5,
                        UserId = Guid.Parse("71fa4b95-079d-42b1-a2cb-dd26028aed9d"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Gray couch in a great condition",
                        Images = new Image[] { new Image { FileName = "couch2" } },
                        CategoryId = 1,
                        Price = 1200,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Room & Board couch (great condition) - delivery included",
                        Description = "I'm selling my furniture at a discount price. Pick up at Venice. DM me asap.",
                        Images = new Image[]
                        {
                            new Image { FileName = "couch1" },
                            new Image { FileName = "couch2" },
                            new Image { FileName = "couch3" }
                        },
                        Price = 1000,
                        CategoryId = 1,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Designer wear shoes",
                        Description = "",
                        Images = new Image[] { new Image { FileName = "shoes1" } },
                        Price = 100,
                        CategoryId = 5,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Canon 400D (Great Condition)",
                        Description = "",
                        Images = new Image[] { new Image { FileName = "camera1" } },
                        Price = 300,
                        CategoryId = 3,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Nikon D850 for sale",
                        Description = "",
                        Images = new Image[] { new Image { FileName = "camera2" } },
                        Price = 250,
                        CategoryId = 3,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Sectional couch - Delivery available",
                        Description = "No rips no stains no odors",
                        Images = new Image[] { new Image { FileName = "couch3" } },
                        Price = 250,
                        CategoryId = 1,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                    new Listing
                    {
                        Id = Guid.NewGuid(),
                        Title = "Brown leather shoes",
                        Description = "No rips no stains no odors",
                        Images = new Image[] { new Image { FileName = "shoes2" } },
                        Price = 250,
                        CategoryId = 5,
                        UserId = Guid.Parse("90cf5e6f-9725-4449-bfec-cf688da1eec4"),
                        Location = new Location
                        {
                            Latitude = 37.78825,
                            Longitude = -122.4324
                        }
                    },
                };
            }
        }

        public static void SeedCategory(MigrationBuilder migrationBuilder)
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
    }
}
