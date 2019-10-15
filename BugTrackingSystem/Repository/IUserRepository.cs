using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackingSystem.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Add(User user);
        Task Delete(int id);
        Task<User> Update(User user);
    }
}
