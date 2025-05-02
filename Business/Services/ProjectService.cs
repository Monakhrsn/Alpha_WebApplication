using System.Linq.Expressions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Domain.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        var projectEntity = formData.MapTo<ProjectEntity>();
       
        var result = await _projectRepository.AddAsync(projectEntity);
        
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
    
    public async Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync(string userId)
    {
        
        var response = await _projectRepository.GetAllAsync<ProjectEntity>
            (
                selector: s => s,
              orderByDescending: true, 
              sortBy: s => s.Created, 
              where: s => s.UserId == userId,
              include => include.User,
              include => include.Client
            );
        var result = response.Result!.Select(p =>
        {
            var target = p.MapTo<Project>();

            target.Client = p.Client.MapTo<Client>();
            target.User = p.Client.MapTo<User>();
            return target;
        });
        
        return new ProjectResult<IEnumerable<Project>>{ Succeeded = true, StatusCode = 200, Result = result };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync(string userId, bool isCompleted)
    {
        Expression<Func<ProjectEntity, bool>> whereClause;
        var today = DateTime.UtcNow.Date;

        if (isCompleted)
            whereClause = p => p.EndDate != null && p.EndDate <= today && userId == p.UserId;
        else
            whereClause = p => (p.EndDate == null || p.EndDate > today) && userId == p.UserId;
        
        var response = await _projectRepository.GetAllAsync<ProjectEntity>
        (
            selector: s => s,
            orderByDescending: true, 
            sortBy: s => s.Created, 
            where: whereClause,
            include => include.User,
            include => include.Client
        );
        var result = response.Result!.Select(p =>
        {
            var target = p.MapTo<Project>();

            target.Client = p.Client.MapTo<Client>();
            target.User = p.Client.MapTo<User>();
            return target;
        });
        
        return new ProjectResult<IEnumerable<Project>>{ Succeeded = true, StatusCode = 200, Result = result };
    }

    public async Task<ProjectResult<int>> GetProjectsCountAsync(string userId)
    {
        
        var result = await _projectRepository.GetCountAsync(userId);
        
        return result.Succeeded
            ? new ProjectResult<int> { Succeeded = true, StatusCode = 201, Result = result.Result}
            : new ProjectResult<int> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<ProjectResult<int>> GetProjectsCountAsync(string userId, bool isCompleted)
    {
        var result = await _projectRepository.GetCountAsync(userId, isCompleted);
        
        return result.Succeeded
            ? new ProjectResult<int> { Succeeded = true, StatusCode = 201, Result = result.Result}
            : new ProjectResult<int> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData)
    {
        var projectEntityResult = await _projectRepository.GetByIdAsync(formData.Id);

        if (!projectEntityResult.Succeeded)
        {
            return new ProjectResult { Succeeded = false, StatusCode = 404, Error = "Project not found." };
        }
        
        var projectEntity = projectEntityResult.Result;

        if (projectEntity != null)
        {
            projectEntity.ProjectName = formData.ProjectName;
            projectEntity.Description = formData.Description;
            projectEntity.StartDate = formData.StartDate;
            projectEntity.EndDate = formData.EndDate;
            projectEntity.Budget = formData.Budget;
            projectEntity.ClientId = formData.ClientId;

            if (formData.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}_{formData.Image.FileName}";
                var filePath = Path.Combine("wwwroot/uploads/projects", fileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.Image.CopyToAsync(stream);
                }

                // Save the relative path to database
                projectEntity.Image = $"/uploads/projects/{fileName}";
            }

            var updateResult = await _projectRepository.UpdateAsync(projectEntity);

            if (!updateResult.Succeeded)
                return new ProjectResult
                    { Succeeded = false, StatusCode = updateResult.StatusCode, Error = updateResult.Error };
        }

        return new ProjectResult { Succeeded = true, StatusCode = 200 };
        
    }

    public async Task<ProjectResult<bool>> DeleteProjectAsync(string userId)
    {
        var projectEntityResult = await _projectRepository.GetByIdAsync(userId);
        
        if (!projectEntityResult.Succeeded)
        {
            return new ProjectResult<bool> { Succeeded = false, StatusCode = 404, Error = "Project not found." };
        }

        var deleteResult = await _projectRepository.DeleteAsync(projectEntityResult.Result!);

        if (!deleteResult.Succeeded)
        {
            return new ProjectResult<bool> { Succeeded = false, StatusCode = 500, Error = deleteResult.Error };
        }
        
        return new ProjectResult<bool> { Succeeded = true, StatusCode = 200 };
    }
}