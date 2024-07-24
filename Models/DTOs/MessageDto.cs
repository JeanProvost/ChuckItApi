using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChuckItApi.Models.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public int MyProperty { get; set; }

        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public UserDto FromUser { get; set; }
        public UserDto ToUser { get; set; }

        [ForeignKey("Listing")]
        public Guid ListingId { get; set; }

    }
}
