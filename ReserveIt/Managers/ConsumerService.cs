using Microsoft.EntityFrameworkCore;
using ReserveIt.Data;
using ReserveIt.Models;
using ReserveIt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public class ConsumerService
    {
        private ResContext _context;

        public ConsumerService(ResContext resContext)
        {
            _context = resContext;
        }
        public async Task<ConsumerUser> GetByAuthenticatedUser(User authenticatedUser)
        {
            return await _context.Consumers.Include(con => con.AuthenticatedUser)
                .SingleOrDefaultAsync(consumer => consumer.AuthenticatedUser == authenticatedUser);
        }
        public async Task<ConsumerUser> GetByUserId(int userId)
        {
            return await _context.Consumers.Include(con => con.AuthenticatedUser)
                .SingleOrDefaultAsync(consumer => consumer.AuthenticatedUser.Id == userId);
        }
    }
}
