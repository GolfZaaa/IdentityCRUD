using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
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

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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
            var result = await roleManager.Roles.ToListAsync();
            return Ok(result);
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update(RoleUpdateDto roleUpdateDto)
        {
            var identityRole = await roleManager.FindByNameAsync(roleUpdateDto.RoleName);


            if (identityRole == null) return NotFound();


            identityRole.Name = roleUpdateDto.UpdateName;
            identityRole.NormalizedName = roleManager.NormalizeKey(roleUpdateDto.UpdateName);


            var result = await roleManager.UpdateAsync(identityRole);


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



        [HttpPost("[action]")]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var identityRole = new IdentityRole
            {
                Name = roleDto.RoleName,
                NormalizedName = roleManager.NormalizeKey(roleDto.RoleName)
            };


            var result = await roleManager.CreateAsync(identityRole);


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




        [HttpDelete]
        public async Task<IActionResult> Delete(RoleDto roleDto)
        {
            var identityRole = await roleManager.FindByNameAsync(roleDto.RoleName);

            if (identityRole == null) return NotFound();

            //ตรวจสอบมีผู้ใช้บทบาทนี้หรือไม่
            var usersInRole = await userManager.GetUsersInRoleAsync(roleDto.RoleName);
            if (usersInRole.Count != 0) return BadRequest();


            var result = await roleManager.DeleteAsync(identityRole);


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
