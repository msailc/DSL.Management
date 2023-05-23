using DSLManagement.Models;

namespace DSLManagement.Services;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString()); // Identity User is not guid
    }
    
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }


    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}