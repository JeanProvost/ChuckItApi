using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using ChuckItApi.Data;
using ChuckItApi.Models;
using ChuckItApi.Models.DTOs;
using ChuckItApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChuckItApi.Services
{
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAmazonS3 _s3CLient;
        private readonly string _bucketName;

        public ListingService(ApplicationDbContext context, IAmazonS3 s3CLient)
        {
            _context = context;
            _s3CLient = s3CLient;
            _bucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
        }

        public async Task<IEnumerable<ListingDto>> GetAllListingsAsync()
        {
            var listings = await _context.Listings
                .Include(l => l.Category)
                .Include(l => l.Location)
                .Include(l => l.User)
                .Include(l => l.Images)
                .ToListAsync();

            return listings.Select((Func<Listing, ListingDto>)(l => new ListingDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                Price = l.Price,
                CategoryId = l.CategoryId,
                CategoryName = l.Category.Name,
                Location = l.Location != null ? new LocationDto
                {
                    Latitude = l.Location.Latitude,
                    Longitude = l.Location.Longitude
                } : null,
                ImageFileNames = l.Images.Select(img => img.FileName).ToList(),
                UserName = l.User.UserName
            }));
        }

        public async Task<ListingDto> GetListingByIdAsync(Guid id)
        {
            var listing = await _context.Listings
                .Include(l => l.Category)
                .Include(l => l.Location)
                .Include(l => l.User)
                .Include(l => l.Images)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (listing == null) return null;

            return new ListingDto
            {
                Id = listing.Id,
                Title = listing.Title,
                CategoryId = listing.CategoryId,
                CategoryName = listing.Category.Name,
                Description = listing.Description,
                ImageFileNames = listing.Images.Select(img => img.FileName).ToList(),
                Location = listing.Location != null ? new LocationDto
                {
                    Latitude = listing.Location.Latitude,
                    Longitude = listing.Location.Longitude
                } : null,
                UserId = listing.UserId,
                Price = listing.Price,
                UserName = listing.User.UserName
            };
        }

        public async Task<ListingDto> CreateListingAsync(ListingDto listingDto)
        {
            var listing = new Listing
            {
                Id = Guid.NewGuid(),
                Title = listingDto.Title,
                Description = listingDto.Description,
                CategoryId = listingDto.CategoryId,
                Price = listingDto.Price,
                Images = new List<Image>(),
            };

            if(listingDto.Location != null)
            {
                listing.Location = new Location
                {
                    Latitude = listingDto.Location.Latitude,
                    Longitude = listingDto.Location.Longitude
                };
            }

            foreach (var imageFileName in listingDto.ImageFileNames)
            {
                using (var imageStream = GetImageStream(imageFileName))
                {
                    var s3Url = await UploadImageToS3Async(imageStream, imageFileName);
                    listing.Images.Add(new Image { FileName = s3Url });
                }
            }

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            listingDto.Id = listing.Id;
            listingDto.ImageFileNames = listing.Images.Select(img => img.FileName).ToList();

            return listingDto;
        }

        public async Task<bool> UpdateListingAsync(ListingDto listingDto)
        {
            var listing = await _context.Listings
                .Include(l => l.Images)
                .FirstOrDefaultAsync(l => l.Id == listingDto.Id);

            if (listing == null) return false;

            listing.Title = listingDto.Title;
            listing.Description = listingDto.Description;
            listing.Price = listingDto.Price;
            listing.CategoryId = listingDto.CategoryId;

            if(listingDto.Location != null){
                if(listing.Location == null)
                {
                    listing.Location = new Location
                    {
                        Latitude = listingDto.Location.Latitude,
                        Longitude = listingDto.Location.Longitude
                    };
                }
                else
                {
                    listing.Location.Latitude = listingDto.Location.Latitude;
                    listing.Location.Longitude = listingDto.Location.Longitude;
                }
            }
            listing.Images.Clear();
            listing.Images = listingDto.ImageFileNames.Select(fileName => new Image { FileName = fileName }).ToList();

            _context.Listings.Update(listing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteListingAsync(Guid id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null) return false;

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> UploadImageToS3Async(Stream imageStream, string fileName)
        {
            var fileTransferUtility = new TransferUtility(_s3CLient);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = imageStream,
                Key = fileName,
                BucketName = _bucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            await fileTransferUtility.UploadAsync(uploadRequest);

            string s3Url = $"https://{uploadRequest.BucketName}.s3.amazonaws.com/{fileName}";

            return s3Url;
        }

        private Stream GetImageStream(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            return new MemoryStream(imageBytes);
        }
    }
}
