using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCRUD.Services.RoleService
{
    public class RoleService : ControllerBase, IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<object> CreateRoleAsync(RoleDto roleDto)
        {
            var identityRole = new IdentityRole
            {
                Name = roleDto.RoleName,
                NormalizedName = _roleManager.NormalizeKey(roleDto.RoleName)
            };


            var result = await _roleManager.CreateAsync(identityRole);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }


            return StatusCode(201);
        }

        public async Task<object> DeleteRoleAsync(RoleDto roleDto)
        {
            var identityRole = await _roleManager.FindByNameAsync(roleDto.RoleName);

            if (identityRole == null) return NotFound();

            //ตรวจสอบมีผู้ใช้บทบาทนี้หรือไม่
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleDto.RoleName);
            if (usersInRole.Count != 0) return BadRequest();


            var result = await _roleManager.DeleteAsync(identityRole);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return StatusCode(201);
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var result = await _roleManager.Roles.ToListAsync();
            return (result);
        }

        public async Task<object> UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
        {
            var identityRole = await _roleManager.FindByNameAsync(roleUpdateDto.RoleName);


            if (identityRole == null) return NotFound();


            identityRole.Name = roleUpdateDto.UpdateName;
            identityRole.NormalizedName = _roleManager.NormalizeKey(roleUpdateDto.UpdateName);


            var result = await _roleManager.UpdateAsync(identityRole);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return StatusCode(201);
        }
    }
}
