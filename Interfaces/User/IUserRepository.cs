using DSLManagement.Models;
using DSLManagement.Views;

namespace DSLManagement.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserView>> GetUsersAsync();
        Task<UserView> GetUserAsync(Guid id);
        Task<UserView> GetUserByUsernameAsync(string username);
        Task DeleteUserAsync(Guid id);
    }
}