using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BugTrackingSystem.Models;
using BugTrackingSystem.Repository;

namespace BugTrackingSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Add(User user)
        {
            return await _userRepository.Add(user);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<User> Get(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}