using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DSLManagement.Models;
using DSLManagement.Views;

namespace DSLManagement.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);
        Task<LoginResult> LoginAsync(LoginRequest request);
    }
}