using E_Phone.BLL.DTOs.Auth;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace E_Phone.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            TokenDTO tokenDTO = await _authService.LoginAsync(login);

            return Ok(tokenDTO.Token);
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = "User")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Çıkış işleminiz başarılıdır.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            await _authService.RegisterAsync(registerDTO);

            return Ok();
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = "User")]
        public async Task<IActionResult> UserInformation()
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            string token = tokenHeader.Split(' ')[1];
            GetUserDTO getUserDTO = await _authService.GetUserAsync(token);

            return Ok(getUserDTO);
        }

        [HttpPut("user")]
        [Authorize(AuthenticationSchemes = "User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            string token = tokenHeader.Split(' ')[1];
            await _authService.UpdateUserAsync(updateUserDTO, token);

            return Ok();
        }
    }
}
