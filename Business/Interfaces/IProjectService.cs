using Business.Models;
using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync(string userId);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync(string userId, bool isCompleted);
    Task<ProjectResult<int>> GetProjectsCountAsync(string userId);
    Task<ProjectResult<int>> GetProjectsCountAsync(string userId, bool isCompleted);
    Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData);
    Task<ProjectResult<bool>> DeleteProjectAsync(string userId);
}