using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Repository
{
    public interface IUserTaskRepository
    {
        Task<IEnumerable<UsersTask>> GetAll();
        Task<UsersTask> Get(int id);
        Task<UsersTask> Add(UsersTask usersTask);
        Task Delete(int id);
        Task<UsersTask> Update(UsersTask usersTask);
    }
}
