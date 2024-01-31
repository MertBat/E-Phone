using E_Phone.BLL.DTOs.Auth;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace E_Phone.API.Controllers
{   /// <summary>
    /// Kullanıcı işlemleri
    /// </summary>
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Kullanıcı giriş işlemi
        /// </summary>
        /// <param name="login">E-mail adresi ve şifre girildiğinde token döndürür.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            TokenDTO tokenDTO = await _authService.LoginAsync(login);

            return Ok(tokenDTO.Token);
        }

        /// <summary>
        /// Kullanıcı çıkış işlemi
        /// </summary>
        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = "User")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Çıkış işleminiz başarılıdır.");
        }

        /// <summary>
        /// Kullanıcı kayıt işlemi
        /// </summary>
        /// <param name="registerDTO">
        /// <strong>email (email adresi):</strong> Geçerli bir email adresi olmalıdır. <br/>
        /// <strong>password (şifre):</strong> 6 karekter, büyük harf, küçük harf ve sayıdan oluşmalıdır. <br/>
        /// <strong>confirmPassword (şifre tekrarı):</strong> Girilmesi zorunludur. Password ile aynı olmalıdır.<br/>
        /// <strong>name (isim):</strong> Maksimum 50 karekter olmalıdır. <br/>
        /// <strong>surname(soyisim):</strong> Maksimum 50 karekter olmalıdır. 
        /// </param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            await _authService.RegisterAsync(registerDTO);

            return Ok();
        }

        /// <summary>
        /// Kullanıcı bilgileri
        /// </summary>
        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = "User")]
        public async Task<IActionResult> UserInformation()
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            string token = tokenHeader.Split(' ')[1];
            GetUserDTO getUserDTO = await _authService.GetUserAsync(token);

            return Ok(getUserDTO);
        }

        /// <summary>
        /// Kullanıcı güncelleme işlemi
        /// </summary>
        /// <param name="updateUserDTO">
        /// <strong>email (email adresi):</strong> Geçerli bir email adresi olmalıdır. <br/>
        /// <strong>password (şifre):</strong> Güncel şifre girilmelidir. <br/>
        /// <strong>Newpassword (şifre):</strong> 6 karekter, büyük harf, küçük harf ve sayıdan oluşmalıdır. <br/>
        /// <strong>confirmNewPassword (şifre tekrarı):</strong> Girilmesi zorunludur. Password ile aynı olmalıdır.<br/> 
        /// <strong>name (isim):</strong> Maksimum 50 karekter olmalıdır. <br/> 
        /// <strong>surname(soyisim):</strong> Maksimum 50 karekter olmalıdır. 
        /// </param>
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
