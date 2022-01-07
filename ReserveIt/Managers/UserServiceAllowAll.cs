using ReserveIt.Data;
using ReserveIt.Models.Request;
using ReserveIt.Utilities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserveIt.Models;

namespace ReserveIt.Managers
{
    public class UserServiceAllowAll : UserService, IUserService
    {
        private ResContext _context;
        
        public UserServiceAllowAll(ResContext resContext) : base(resContext)
        {
            _context = resContext;
        }

        public override async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            //if (user == null)
            //    return null;
            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;

            return user;
        }
    }
}
