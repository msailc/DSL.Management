using DSLManagement.Models;

namespace DSLManagement.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task DeleteUserAsync(Guid id);
    }
}