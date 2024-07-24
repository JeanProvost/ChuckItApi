using ChuckItApi.Data;
using ChuckItApi.Models;
using ChuckItApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckItApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Messages>> GetMessagesForUser(Guid userId)
        {
            return await _context.Messages
                .Include(m => m.FromUser)
                .Include(m => m.ToUserId)
                .Where(m => m.FromUserId == userId || m.ToUserId == userId)
                .ToListAsync();
        }

        public async Task AddMessage(Messages message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}