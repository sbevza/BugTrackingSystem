using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project> Get(int id);
        Task<Project> Add(Project project);
        Task Delete(int id);
        Task<Project> Update(Project project);
    }
}
