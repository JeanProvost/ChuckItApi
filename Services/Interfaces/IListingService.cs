using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChuckItApi.Models;
using ChuckItApi.Models.DTOs;

namespace ChuckItApi.Services.Interfaces
{
    public interface IListingService
    {
        Task<IEnumerable<ListingDto>> GetAllListingsAsync();
        Task<ListingDto> GetListingByIdAsync(Guid id);
        Task<ListingDto> CreateListingAsync(ListingDto listingDto);
        Task<bool> UpdateListingAsync(ListingDto listingDto);
        Task<bool> DeleteListingAsync(Guid id);
    }
}
