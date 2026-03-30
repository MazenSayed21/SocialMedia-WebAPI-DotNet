using SOCIALIZE.DTOs;
using SOCIALIZE.Models;
namespace SOCIALIZE.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(registerDTO user);
    }
}
