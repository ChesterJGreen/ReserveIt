using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt.Managers
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

        Task<IEnumerable<User>> GetAllReadOnly();

        Task<User> GetById(int id);

        Task<User> Create(User user);

        Task Update(User user, string password);

        Task<bool> Delete(int id);
    }
}
