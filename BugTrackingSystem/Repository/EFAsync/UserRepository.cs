using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using NLog;

namespace BugTrackingSystem.Repository.EFAsync
{
    public class UserRepository : IUserRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = new List<User>();
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("GetAll(). Getting All Users.");
                result = await ctx.Users.ToListAsync();
                _logger.Debug("GetAll(). Get amount {0} users.", result.Count);
            }
            return  result;
        }

        public async Task<User> Get(int id)
        {
            User result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Get(). Getting user by id: {0}.", id);
                result = await ctx.Users.FirstOrDefaultAsync(p => p.Id == id);
            }
            _logger.Debug("Get(). User UserLogin = {0} have get.", result.UserLogin);

            return result;
        }

        public async Task<User> Add(User user)
        {
            User result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Add(). Adding user: {0}.", user.UserLogin);
                result = ctx.Users.Add(user);
                await ctx.SaveChangesAsync();
            }
            _logger.Debug("Add(). User: {0} have added.", user.UserLogin);
            return result;
        }

        public async Task Delete(int id)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Delete(). Deleting user by id: {0}.", id);
                var user = await ctx.Users.FirstOrDefaultAsync(p => p.Id == id);
                ctx.Entry(user).State = EntityState.Deleted;
                var a = await ctx.SaveChangesAsync();
            }
            _logger.Debug("Delete(). User deleted by id: {0}.", id);
        }

        public async Task<User> Update(User user)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Update(). Updating user: {0}.", user.UserLogin);
                ctx.Entry(user).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            _logger.Debug("Update(). User {0} have updated.", user.UserLogin);
            return user;
        }
    }
}