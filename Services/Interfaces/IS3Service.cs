using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChuckItApi.Services.Interfaces
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<string> GetFileUrlAsync(string fileName);
    }

}
