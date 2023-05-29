using DSLManagement.Models;
using DSLManagement.Views;

namespace DSLManagement.Services;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserView>> GetUsersAsync()
    {
        return await _context.Users
            .Include(p => p.Pipelines)
            .Select(u => new UserView
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                PipelinesCount = u.Pipelines.Count,
                PipelineStepsCount = u.Pipelines.Sum(p => p.Steps.Count > 0 ? p.Steps.Count : 0),
                SuccessPipelinesCount = u.PipelineExecutions.Count(e => e.Success),
                FailedPipelinesCount = u.PipelineExecutions.Count(e => !e.Success),
                PipelineExecutions = u.PipelineExecutions.Select(e => new UserPipelineExecutionView
                {
                    PipelineExecutionId = e.Id,
                    PipelineName = e.Pipeline.Name,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Success = e.Success,
                }).ToList(),
                Pipelines = u.Pipelines.Select(p => new UserPipelineView
                {
                    PipelineId = p.Id,
                    Name = p.Name
                }).ToList()
            })
            .ToListAsync<UserView>();
    }

    public async Task<UserView> GetUserAsync(Guid id)
    {
        string idString = id.ToString();

        var user = await _context.Users
            .Include(p => p.Pipelines)
            .ThenInclude(s => s.Steps)
            .ThenInclude(pr => pr.Parameters)
            .Include(e => e.PipelineExecutions)
            .FirstOrDefaultAsync(u => u.Id == idString);

        var userView = new UserView
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PipelinesCount = user.Pipelines.Count,
            PipelineStepsCount = user.Pipelines.Sum(p => p.Steps.Count > 0 ? p.Steps.Count : 0),
            SuccessPipelinesCount = user.PipelineExecutions.Count(e => e.Success),
            FailedPipelinesCount = user.PipelineExecutions.Count(e => !e.Success),
            PipelineExecutions = user.PipelineExecutions.Select(e => new UserPipelineExecutionView
            {
                PipelineExecutionId = e.Id,
                PipelineName = e.Pipeline.Name,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Success = e.Success,
            }).ToList(),
            Pipelines = user.Pipelines.Select(p => new UserPipelineView
            {
                PipelineId = p.Id,
                Name = p.Name,
                Steps = p.Steps.Select(s => new PipelineStepView
                {
                    StepId = s.Id,
                    Command = s.Command,
                    Parameters = s.Parameters.Select(pr => new PipelineStepParameterView
                    {
                        ParameterId = pr.Id,
                        Name = pr.Name,
                        Value = pr.Value
                    }).ToList()
                }).ToList()
            }).ToList()
        };

        userView.PipelinesCount = userView.Pipelines.Count;
        userView.PipelineStepsCount = userView.Pipelines.Sum(p => p.Steps.Count);

        return userView;
    }
    
    public async Task<UserView> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Pipelines)
            .Where(u => u.UserName == username)
            .Select(u => new UserView
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                PipelinesCount = u.Pipelines.Count,
                PipelineStepsCount = u.Pipelines.Sum(p => p.Steps.Count > 0 ? p.Steps.Count : 0),
                SuccessPipelinesCount = u.PipelineExecutions.Count(e => e.Success),
                FailedPipelinesCount = u.PipelineExecutions.Count(e => !e.Success),
                PipelineExecutions = u.PipelineExecutions.Select(e => new UserPipelineExecutionView
                {
                    PipelineExecutionId = e.Id,
                    PipelineName = e.Pipeline.Name,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Success = e.Success,
                }).ToList(),
                Pipelines = u.Pipelines.Select(p => new UserPipelineView
                {
                    PipelineId = p.Id,
                    Name = p.Name,
                    Steps = p.Steps.Select(s => new PipelineStepView
                    {
                        StepId = s.Id,
                        Command = s.Command,
                        Parameters = s.Parameters.Select(pr => new PipelineStepParameterView
                        {
                            ParameterId = pr.Id,
                            Name = pr.Name,
                            Value = pr.Value
                        }).ToList()
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync<UserView>();
    }


    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}