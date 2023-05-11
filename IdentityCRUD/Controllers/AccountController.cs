using IdentityCRUD.DTOs;
using IdentityCRUD.Models;
using IdentityCRUD.Services;
using IdentityCRUD.Services.AccService;
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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _accountService.GetUserAsync();
            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var regis = await _accountService.RegisterAsync(registerDto);

            return Ok(regis);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var login = await _accountService.LoginAsync(loginDto);
            return Ok(login);
        }


        [HttpGet("TestAdminRole"), Authorize(Roles = "Admin")]
        public IActionResult test()
        {
            return Ok("Authorize Success");
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> DisplayTokenDetails()
        {
            var Display = await _accountService.GetTokenDetailAsync();
            return Ok(Display);
        }



    }
}
