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
        private IUserService _userService;


        public ConsumerService(ResContext resContext, IUserService userService)
        {
            _context = resContext;
            _userService = userService
        }
        public async Task<ConsumerUser> GetByUserId(int userId)
        {
            return await _context.Consumers.Include(con => con.AuthenticatedUser)
                .SingleOrDefaultAsync(consumer => consumer.AuthenticatedUser.Id == userId);
        }
        public async Task<bool> UpdateUsername(int userId, string username)
        {
            return await _userService.UpdateUsername(userId, username);
        }
        public async Task<bool> UpdateFirstLastName(int userId, string firstName, string lastName)
        {
            return await _userService.UpdateFirstLastName(userId, firstName, lastName);
        }
    }
}
