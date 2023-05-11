using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
using IdentityCRUD.Services.RoleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRoleService _roleService;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IRoleService roleService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _roleService = roleService;
        }

        /// Start MyEdit
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetRole()
        //{
        //    return Ok(await roleManager.Roles.ToListAsync());
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateRole(RoleDto request)
        //{
        //    var role = new ApplicationRole
        //    {
        //        Name = request.RoleName,
        //    };

        //    try
        //    {
        //        var roleuser = await roleManager.SetRoleNameAsync(role, request.RoleName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error");
        //    }

        //    //var result = await userManager.CreateAsync(user, registerDto.Password);

        //    var result = await roleManager.CreateAsync(role);

        //    return Ok(result);
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> UpdateRole(RoleUpdateDto request)
        //{
        //    var update = await roleManager.FindByNameAsync(request.UpdateName);

        //    if (update == null) { return BadRequest("error"); }

        //    update.Name = request.UpdateName;

        //    var result = await roleManager.UpdateAsync(update);

        //    return Ok(result);

        //}

        // End MyEdit


        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }






        [HttpPost("[action]")]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var Cre = await _roleService.CreateRoleAsync(roleDto);

            return Ok(Cre);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(RoleUpdateDto roleUpdateDto)
        {
            var Upda = await _roleService.UpdateRoleAsync(roleUpdateDto);

            return Ok(Upda);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(RoleDto roleDto)
        {
           var Del = await _roleService.DeleteRoleAsync(roleDto);

            return Ok(Del);
        }


    }
}
