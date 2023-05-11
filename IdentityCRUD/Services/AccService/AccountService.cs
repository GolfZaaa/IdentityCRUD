using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityCRUD.Services.AccService
{
    public class AccountService : ControllerBase, IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService( UserManager<ApplicationUser> userManager, TokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> GetTokenDetailAsync()
        {

            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var favorite = User.FindFirstValue("Favorite");
            //var userName = User.FindFirstValue(ClaimTypes.Name);
            //var email = await _userManager.GetEmailAsync(user);
            //var token = HttpContext.GetTokenAsync("access_token");
            //return Ok(new { userName, email, favorite, token });

            var favorite = string.Empty;
            var userName = string.Empty;
            var email = string.Empty;
            var token = string.Empty;

            if  (_httpContextAccessor.HttpContext is not null)
            {
                favorite =  _httpContextAccessor.HttpContext.User.FindFirstValue("Favorite");
                userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            }

            return (new { userName, email, favorite, token });

        }

        public async Task<List<object>> GetUserAsync()
        {
            var result = await _userManager.Users.ToListAsync();

            List<Object> users = new();

            foreach (var user in result)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                users.Add(new { user.UserName, userRole });
            }

            return (users);
        }

        public async Task<object> LoginAsync(LoginDto loginDto)
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

        public async Task<object> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return (ValidateError(result));

            try
            {
                var resultRole = await _userManager.AddToRoleAsync(user, registerDto.Role);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Role Not Found");
            }

            return StatusCode(StatusCodes.Status201Created);
        }


        private Object ValidateError(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem();
        }


    }
}
