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
            .FirstOrDefaultAsync(u => u.Id == idString);

        var userView = new UserView
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Token = user.Token,
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
                PipelineStepsCount = u.Pipelines.Sum(p => p.Steps.Count),
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