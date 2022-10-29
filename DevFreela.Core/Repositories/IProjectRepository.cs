using DevFreela.Core.DTO;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.Models;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1);
        Task<Project> GetProjectByIdAsync(int id);

        Task<int> AddAsync(Project project);
        Task DeleteAsync(int id);
        Task StartAsync(Project project);

        Task AddComment(ProjectComment comment);
        Task SaveChangesAsync();        

    }
}
