using E_Phone.BLL.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Abstract
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<GetUserDTO> GetUserAsync(string Token);
        Task UpdateUserAsync(UpdateUserDTO userDTO, string token);
    }
}
