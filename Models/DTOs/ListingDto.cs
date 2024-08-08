using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChuckItApi.Models.DTOs
{
    public class ListingDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public string CategoryName { get; set; }

        public LocationDto Location { get; set; }

        public List<string> ImageFileNames { get; set; } = new List<string>();

        public string UserId { get; set; }

        public string UserName { get; set; }
    }

    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
