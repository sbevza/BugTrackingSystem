using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackingSystem.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project> Get(int id);
        Task<Project> Add(Project project);
        Task Delete(int id);
        Task<Project> Update(Project project);
    }
}
