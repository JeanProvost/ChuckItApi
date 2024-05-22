using System.Collections.Generic;
using System.Threading.Tasks;
using ChuckItApi.Models;

namespace ChuckItApi.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUser(string userId);
        Task AddUserAsync(ApplicationUser user, string password);
    }
}
