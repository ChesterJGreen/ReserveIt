using ReserveIt.Data;
using ReserveIt.Models.Request;
using ReserveIt.Utilities.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserveIt.Models;
using System.Security.Claims;
using ReserveIt.Models.Entities;

namespace ReserveIt.Managers
{
    public class UserService : IUserService
    {
        private ResContext _context;
        
        public UserService(ResContext resContext)
        {
            _context = resContext;
        }

        public bool isThisTheClass() => true;

        public virtual async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            //var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            var user = await GetByUsernameWithAccessControl(username);

            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            _ = _context.Roles.ToHashSet();
            _ = _context.Groups.Include(group => group.GroupRoleMappings).ToHashSet();

            return user;
        }

        public async Task<User> GetByUsernameWithAccessControl(string username)
        {
            return await _context.Users
                .Include(user => user.UserRoles)
                .Include(user => user.UserGroups)
                .SingleOrDefaultAsync(user => user.Username == username);
            
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
        public List<Claim> BuildUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            var userRoles = user.UserRoles.Select(map => map.Role);
            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role.Name));
            IEnumerable<GroupRoleMapping> userGroupRoleMappings = user.UserGroups.SelectMany(map => map.Group.GroupRoleMappings);

            var userGroupRoleClaims = userGroupRoleMappings.Select(map => map.Role).Select(role => new Claim(ClaimTypes.Role, role.Name));

            claims.AddRange(userRoleClaims);
            claims.AddRange(userGroupRoleClaims);

            return claims;
        }
        /// <summary>
        /// update a user's username
        /// </summary>
        /// <param name="userId">the user ID to operate on</param>
        /// <param name="username">the new username</param>
        /// <returns>true if successful; otherwise, false</returns>
        public async Task<bool> UpdateUsername(int userId, string username)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return false;

            if (user.Username == username)
                return true;

            if (await IsUsernameTaken(username))
                return false;

            user.Username = username;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateFirstLastName(int userId, string firstName, string lastName)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return false;
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return false;

            user.FirstName = firstName;
            user.LastName = lastName;
            await _context.SaveChangesAsync();
            return true;
        }   
        private async Task<bool> IsUsernameTaken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return true;

            bool alreadyExists = await _context.Users.AsNoTracking().AnyAsync(x => x.Username == username);
            return alreadyExists;
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
