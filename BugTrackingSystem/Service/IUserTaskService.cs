using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Service
{
    public interface IUserTaskService
    {
        Task<IEnumerable<UsersTask>> GetAll();
        Task<UsersTask> Get(int id);
        Task<UsersTask> Add(UsersTask user);
        Task Delete(int id);
        Task<UsersTask> Update(UsersTask user);
    }
}
