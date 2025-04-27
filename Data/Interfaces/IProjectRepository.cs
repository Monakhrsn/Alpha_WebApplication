
using Data.Entities;
using Data.Models;
using Domain.Models;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    public Task<RepositoryResult<int>> GetCountAsync();
    public Task<RepositoryResult<int>> GetCountAsync(bool isCompleted);
}