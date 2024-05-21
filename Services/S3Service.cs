using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChuckItApi.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        } 

        public async Task<string> GetFileUrlAsync(string fileName)
        {
            var request = new Amazon.S3.Model.GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Expires = DateTime.UtcNow.AddMinutes(60)

            };

            return _s3Client.GetPreSignedURL(request);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            var key = Guid.NewGuid().ToString();

            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = key,
                    BucketName = _bucketName,
                    ContentType = file.ContentType
                };

                await fileTransferUtility.UploadAsync(uploadRequest);
                return key;
            }
        }
    }
}
