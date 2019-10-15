using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using NLog;

namespace BugTrackingSystem.Repository.EFAsync
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<UsersTask>> GetAll()
        {
            var result = new List<UsersTask>();
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("GetAll(). Getting All Tasks.");
                result = await ctx.UsersTasks.Include(u => u.Project).Include(u => u.User).ToListAsync();
            }
            _logger.Debug("GetAll(). Get amount {0} tasks.", result.Count);
            return result;
        }

        public async Task<UsersTask> Get(int id)
        {
            UsersTask result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Get(). Getting task by id: {0}.", id);
                result = await ctx.UsersTasks.Include(u => u.Project).Include(u => u.User).FirstOrDefaultAsync(p => p.Id == id);
            }
            _logger.Debug("Get(). Task Name = {0} have get.", result.Topic);
            return result;
        }

        public async Task<UsersTask> Add(UsersTask usersTask)
        {
            UsersTask result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Add(). Adding task {0}.", usersTask.Topic);
                result = ctx.UsersTasks.Add(usersTask);
                await ctx.SaveChangesAsync();
                _logger.Debug("Add(). Task {0} have added.", usersTask.Topic);
            }

            return result;
        }

        public async Task Delete(int id)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Delete(). Deleting task by id: {0}.", id);
                var usersTask = await ctx.UsersTasks.FirstOrDefaultAsync(p => p.Id == id);
                ctx.Entry(usersTask).State = EntityState.Deleted;
                await ctx.SaveChangesAsync();
                _logger.Debug("Delete(). Task deleted by id: {0}.", id);
            }
        }

        public async Task<UsersTask> Update(UsersTask usersTask)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Update(). Updating task: {0}.", usersTask.Topic);
                ctx.Entry(usersTask).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                _logger.Debug("Update(). Task {0} have updated.", usersTask.Topic);
            }

            return usersTask;
        }
    }
}