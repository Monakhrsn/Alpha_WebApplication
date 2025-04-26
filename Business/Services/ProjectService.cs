

using System.Diagnostics;
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
    
    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync<ProjectEntity>
            (
                selector: s => s,
              orderByDescending: true, 
              sortBy: s => s.Created, 
              where: null,
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
    
    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
        (
            where: x => x.Id == id,
            include => include.User,
            include => include.Status,
            include => include.Client
        );

        return response.Succeeded
            ? new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"Project '{id}' was not found." };
    }
}