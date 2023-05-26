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
                Pipelines = u.Pipelines.Select(p => new PipelineView
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
            .FirstOrDefaultAsync(u => u.Id == idString); 

        var userView = new UserView
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Token = user.Token,
            Pipelines = user.Pipelines.Select(p => new PipelineView
            {
                PipelineId = p.Id,
                Name = p.Name
            }).ToList()
        };

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
                Pipelines = u.Pipelines.Select(p => new PipelineView
                {
                    PipelineId = p.Id,
                    Name = p.Name
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