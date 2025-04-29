
using Data.Entities;
using Data.Models;
using Domain.Models;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    public Task<RepositoryResult<int>> GetCountAsync(string userId);
    public Task<RepositoryResult<int>> GetCountAsync(string userId, bool isCompleted);
    public Task<RepositoryResult<ProjectEntity>> GetByIdAsync(string id);

}