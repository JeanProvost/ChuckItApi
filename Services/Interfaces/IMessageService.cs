using ChuckItApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChuckItApi.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Messages>> GetMessagesForUser(string userId);
        Task AddMessage(Messages message);
    }
}
