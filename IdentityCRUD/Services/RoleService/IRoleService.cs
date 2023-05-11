using IdentityCRUD.DTOs;

namespace IdentityCRUD.Services.RoleService
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<Object> CreateRoleAsync(RoleDto roleDto);
        Task<Object> UpdateRoleAsync(RoleUpdateDto roleUpdateDto);
        Task<Object> DeleteRoleAsync(RoleDto roleDto);
    }
}
