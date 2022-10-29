using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.Models;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private const int PAGE_SIZE = 2;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaConnStr");
        }

        public async Task<PaginationResult<Project>> GetAllAsync(string query = "", int page = 1)
        {
            IQueryable<Project> projects = _dbContext.Projects;

            if (!string.IsNullOrEmpty(query))
            {
                projects = projects
                    .Where(q =>
                        q.Title.Contains(query) ||
                        q.Description.Contains(query));
            }

            return await projects.GetPaged(page, PAGE_SIZE);
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _dbContext.Projects
                .Include(q => q.Client)
                .Include(q => q.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<int> AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            // Moved to UnitOfWork
            // await _dbContext.SaveChangesAsync();
            return project.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Cancel();

            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            // Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "UPDATE Projects set Status = @status, StartedAt = @startedat WHERE Id = @id";
                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, id = project.Id });
            }
        }

        public async Task AddComment(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
