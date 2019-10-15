using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Add(User user);
        Task Delete(int id);
        Task<User> Update(User user);
    }
}
