using DevFreela.Core.DTO;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>> GetAllAsync();

        Task AddSkillFromProject(Project project);
    }
}