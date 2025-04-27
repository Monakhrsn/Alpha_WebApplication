using System.Linq.Expressions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Domain.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        var statusResult = await _statusService.GetStatusByIdAsync(1);

        if (!statusResult.Succeeded || statusResult.Result == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required field are supplied."};

        var projectEntity = formData.MapTo<ProjectEntity>();
        var status = statusResult.Result;
        
        projectEntity.StatusId = status.Id;
        
        var result = await _projectRepository.AddAsync(projectEntity);
        
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
    
    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync(string userId)
    {
        
        var response = await _projectRepository.GetAllAsync<ProjectEntity>
            (
                selector: s => s,
              orderByDescending: true, 
              sortBy: s => s.Created, 
              where: s => s.UserId == userId,
              include => include.User,
              include => include.Status,
              include => include.Client
            );
        var result = response.Result!.Select(p =>
        {
            var target = p.MapTo<Project>();

            target.Client = p.Client.MapTo<Client>();
            target.Status = p.Status.MapTo<Status>();
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
            include => include.Status,
            include => include.Client
        );
        var result = response.Result!.Select(p =>
        {
            var target = p.MapTo<Project>();

            target.Client = p.Client.MapTo<Client>();
            target.Status = p.Status.MapTo<Status>();
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
}