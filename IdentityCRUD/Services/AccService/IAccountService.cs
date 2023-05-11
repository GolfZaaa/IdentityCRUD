using IdentityCRUD.DTOs;

namespace IdentityCRUD.Services.AccService
{
    public interface IAccountService
    {
        Task<List<Object>> GetUserAsync();
        Task<Object> RegisterAsync(RegisterDto registerDto);
        Task<Object> LoginAsync(LoginDto loginDto);
        Task<Object> GetTokenDetailAsync();
    }
}
