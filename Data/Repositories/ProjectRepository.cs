using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    public async Task<RepositoryResult<int>> GetCountAsync()
    {
        try
        {
            var count = await _table.CountAsync();
            return new RepositoryResult<int> { Succeeded = true, StatusCode = 200, Result = count };
        }
        catch (Exception e)
        {
            var message = e.InnerException?.Message ?? e.Message;
            Console.WriteLine("Error fetching count: " + message);

            return new RepositoryResult<int> 
            { 
                Succeeded = false, 
                StatusCode = 500, 
                Error = message 
            };
        }
    }

    public async Task<RepositoryResult<int>> GetCountAsync(bool isCompleted)
    {
        Expression<Func<ProjectEntity, bool>> whereClause;
        var today = DateTime.UtcNow.Date;

        if (isCompleted)
            whereClause = p => p.EndDate != null && p.EndDate <= today;
        else
            whereClause = p => p.EndDate == null || p.EndDate > today;
        
        try
        {
            var count = await _table
                .Where(whereClause)
                .CountAsync();
            
            return new RepositoryResult<int> { Succeeded = true, StatusCode = 200, Result = count };
        }
        catch (Exception e)
        {
            var message = e.InnerException?.Message ?? e.Message;
            Console.WriteLine("Error fetching count: " + message);

            return new RepositoryResult<int> 
            { 
                Succeeded = false, 
                StatusCode = 500, 
                Error = message 
            };
        }
    }
}