using BugTrackingSystem.Models;
using BugTrackingSystem.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackingSystem.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _projectRepository.GetAll();
        }

        public async Task<Project> Get(int id)
        {
            return await _projectRepository.Get(id);
        }

        public async Task<Project> Add(Project project)
        {
            return await _projectRepository.Add(project);
        }

        public async Task Delete(int id)
        {
            await _projectRepository.Delete(id);
        }

        public async Task<Project> Update(Project project)
        {
            return await _projectRepository.Update(project);
        }
    }
}