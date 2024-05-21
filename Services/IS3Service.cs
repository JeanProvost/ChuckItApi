using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChuckItApi.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> GetFileUrlAsync(string fileName);
    }

}
