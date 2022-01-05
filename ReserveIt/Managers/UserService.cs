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
    public class UserService : IUserService
    {
        private ResContext _context;
        
        public UserService(ResContext resContext)
        {
            _context = resContext;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Create(UserAddUpdateRequest request) =>
            await Create(request.Username, request.FirstName, request.LastName, request.Password);
        
        public async Task<User> Create(string username, string firstName, string lastName, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ServiceBadRequestException("Password is required");

            if (await _context.Users.AnyAsync(x => x.Username == username))
                throw new ServiceBadRequestException("Username \"" + username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            User user = new User();
            user.Username = username;
            user.LastName = lastName;
            user.FirstName = firstName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;

        }
        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
                return true;
        }

       public async Task<IEnumerable<User>> GetAllReadOnly()
        {
            return await _context.Users.AsNoTracking().ToArrayAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(User userParam, string password = null)
        {
            var user = await _context.Users.FindAsync(userParam.Id);

                if (user == null)
                throw new ServiceBadRequestException("User not found");

                if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username !=user.Username)
                {
                if (await _context.Users.AnyAsync(x => x.Username == userParam.Username))
                    throw new ServiceBadRequestException("Username " + userParam.Username + "is already taken");
                user.Username = userParam.Username;
                }

            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;
            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            await _context.SaveChangesAsync();

        }
        
        #region Private Static Methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;

            
        }
        #endregion
    }
}
