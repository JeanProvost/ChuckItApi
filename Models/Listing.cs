using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApi.Models
{
    public class Listing
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        public  double Price { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Location Location { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
    /* public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Listing> Listings { get; set; }
    } */

    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public Guid ListingId { get; set; }
        [ForeignKey("ListingId")]
        public Listing Listing { get; set; }
    }

}


