using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuckItApi.Models
{
    public class Messages
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime datetime { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }

    }
}
