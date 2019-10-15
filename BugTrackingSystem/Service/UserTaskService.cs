using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BugTrackingSystem.Models;
using BugTrackingSystem.Repository;

namespace BugTrackingSystem.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskService(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public async Task<UsersTask> Add(UsersTask usersTask)
        {
            return await _userTaskRepository.Add(usersTask);
        }

        public async Task Delete(int id)
        {
            await _userTaskRepository.Delete(id);
        }

        public async Task<UsersTask> Get(int id)
        {
            return await _userTaskRepository.Get(id);
        }

        public async Task<IEnumerable<UsersTask>> GetAll()
        {
            return await _userTaskRepository.GetAll();
        }

        public async Task<UsersTask> Update(UsersTask usersTask)
        {
            return await _userTaskRepository.Update(usersTask);
        }
    }
}