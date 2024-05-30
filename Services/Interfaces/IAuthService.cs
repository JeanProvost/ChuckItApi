using ChuckItApi.Models.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ChuckItApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<string> LoginUserAsync(LoginDto loginDto);
    }
}
