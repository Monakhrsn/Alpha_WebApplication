using Domain.Models;

namespace WebApp.Models;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];

    public AddProjectViewModel AddProjectFormData { get; set; } = new();
    public EditProjectViewModel EditProjectFormData { get; set; } = new();
    public int AllProjectCount { get; set; }
    public int CompletedProjectCount { get; set; }
    public int StartedProjectCount { get; set; }
}