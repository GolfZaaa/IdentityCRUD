using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
using IdentityCRUD.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace IdentityCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(TokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            try
            {
                var resultRole = await _userManager.AddToRoleAsync(user, registerDto.Role);

            }
            catch (Exception ex)
            {
                return BadRequest("Error Role Not Found");
            }


            var result = await _userManager.CreateAsync(user, registerDto.Password);


            if (!result.Succeeded) return Ok(ValidateError(result));


            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);


            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();


            var userDto = new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
            };
            return Ok(userDto);
        }



        private Object ValidateError (IdentityResult result)
        {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }

        [HttpGet("TestAdminRole"), Authorize(Roles = "Admin")]
        public IActionResult test()
        {
            return Ok("Authorize Success");
        }

        [HttpGet("GetMeInBaseController"), Authorize]
        public async Task<IActionResult> GetMyName()
        {
            //var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);


            return Ok(new { user.UserName, roles });
        }



        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> DisplayTokenDetails()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var favorite = User.FindFirstValue("Favorite");
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var email = await _userManager.GetEmailAsync(user);
            var token = HttpContext.GetTokenAsync("access_token");
            return Ok(new { userName, email, favorite, token });
        }



    }
}
