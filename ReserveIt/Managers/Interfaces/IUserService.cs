using ReserveIt.Models;
using ReserveIt.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public interface IUserService
    {
        /// <summary>
        /// authenticate the user with the provided password
        /// </summary>
        /// <param name="username">the username of the user to authenticate</param>
        /// <param name="password">the plain-text password</param>
        /// <returns>null if the authentication failed; otherwise returns the user</returns>
        Task<User> Authenticate(string username, string password);
        /// <summary>
        /// get all users without EF tracking
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllReadOnly();
        /// <summary>
        /// get the user with the supplied ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null if no user found; otherwise the user</returns>
        Task<User> GetById(int id);
        /// <summary>
        /// create the user with the supplied model and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns>the created user</returns>
        /// <exception cref="Utilities.Error.ServiceBadRequestException">thrown if the user or password fail validation</exception>

        Task<User> Create(UserAddUpdateRequest request);
        /// <summary>
        /// create the user with the supplied model and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns>the created user</returns>
        /// <exception cref="Utilities.Error.ServiceBadRequestException">thrown if the user or password fail validation</exception>
        Task<User> Create(string username, string firstName, string lastName, string password);
        /// <summary>
        /// update the user with the supplied model and optional password
        /// </summary>
        /// <param name="user">the user entity with updated values</param>
        /// <param name="password">leave null if the password is not changing</param>
        /// <returns></returns>
        /// <exception cref="Utilities.Error.ServiceBadRequestException">thrown if the user or password fail validation</exception>

        Task Update(User user, string password = null);
        /// <summary>
        /// delete the user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if the user exists and was deleted; false if the user does not exist</returns>
        Task<bool> Delete(int id);

        List<Claim> BuildUserClaims(User user);

        Task<bool> UpdateUsername(int userId, string username);

        Task<bool> UpdateFirstLastName(int userId, string firstName, string lastName);
    }
}
