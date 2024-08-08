using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChuckItApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChuckItApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUser(Guid userId);
        Task AddUserAsync(ApplicationUser user, string password);
    }
}
