using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChuckItApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ChuckItApi.Services.Interfaces;

namespace ChuckItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingsController(IListingService listingSerivce)
        {
            _listingService = listingSerivce;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetListings()
        {
            var listings = await _listingService.GetAllListingsAsync();
            return Ok(listings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingDto>> GetListing(Guid id)
        {
            var listing = await _listingService.GetListingByIdAsync(id);
            if (listing == null) return NotFound();

            return Ok(listing);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ListingDto>> PostListing(ListingDto listingDto)
        {
            var newListing = await _listingService.CreateListingAsync(listingDto);
            return CreatedAtAction(nameof(GetListing), new { id = newListing.Id }, newListing);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateListing(Guid id, ListingDto listingDto)
        {
            if (id != listingDto.Id) return BadRequest();

            var result = await _listingService.UpdateListingAsync(listingDto);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteListing(Guid id)
        {
            var result = await _listingService.DeleteListingAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
