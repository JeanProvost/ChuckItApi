using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace ChuckItApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Listing> Listings { get; set; }
    }
}
