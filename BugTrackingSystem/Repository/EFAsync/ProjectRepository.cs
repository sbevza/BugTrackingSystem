using BugTrackingSystem.Models;
using NLog;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BugTrackingSystem.Repository.EFAsync
{
    public class ProjectRepository : IProjectRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<Project>> GetAll()
        {
            var result = new List<Project>();
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("GetAll(). Getting All Projects.");
                result = await ctx.Projects.ToListAsync();
            }
            _logger.Debug("GetAll(). Get amount {0} projects.", result.Count);
            return result;
        }

        public async Task<Project> Get(int id)
        {
            Project result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Get(). Getting project by id: {0}.", id);
                result = await ctx.Projects.FirstOrDefaultAsync(p => p.Id == id);
            }
            _logger.Debug("Get(). Project Name = {0} have get.", result.Name);
            return result;
        }

        public async Task<Project> Add(Project project)
        {
            Project result = null;
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Add(). Adding project {0}.", project.Name);
                result = ctx.Projects.Add(project);
                await ctx.SaveChangesAsync();
                _logger.Debug("Add(). Project {0} have added.", project.Name);
            }

            return result;
        }

        public async Task Delete(int id)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Delete(). Deleting project by id: {0}.", id);
                var project = await ctx.Projects.FirstOrDefaultAsync(p => p.Id == id);
                ctx.Entry(project).State = EntityState.Deleted;
                await ctx.SaveChangesAsync();
                _logger.Debug("Delete(). Project deleted by id: {0}.", id);
            }
        }

        public async Task<Project> Update(Project project)
        {
            using (var ctx = new BugTrackingContext())
            {
                _logger.Debug("Update(). Updating Project: {0}.", project.Name);
                ctx.Entry(project).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            _logger.Debug("Update(). Project {0} have updated.", project.Name);
            return project;
        }
    }
}