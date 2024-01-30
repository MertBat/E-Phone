using AutoMapper;
using E_Phone.BLL.DTOs.Auth;
using E_Phone.BLL.Handlers.Abstract;
using E_Phone.BLL.Services.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.IRepositories;
using E_Phone.Core.IRepositories.BaseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public AuthService(IBaseRepository<User> userRepository, ITokenHandler tokenHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> GetUserAsync(string token)
        {
            int userId = _tokenHandler.GetIdFromToken(token);
            User user = await _userRepository.GetAsync(u => u.Id == userId);

            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO)
        {
            User user = await _userRepository.GetAsync(u => u.Email == loginDTO.Email);
            if (user != null)
            {
                bool passwordValid = user.Password == passwordHasher(loginDTO.Password);
                if (passwordValid)                
                    return _tokenHandler.CreateAccessToken(user);   
            }

            throw new ArgumentException("Email veya şifre yanlış");
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            bool isUserExist = await _userRepository.AnyAsync(u => u.Email == registerDTO.Email);
            if (isUserExist)
                throw new ArgumentException("Bu email adresine ait kullanıcı vardır.");

            registerDTO.Password = passwordHasher(registerDTO.Password);
            User user = _mapper.Map<User>(registerDTO);

            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO updateUserDTO, string token)
        {
            int userId = _tokenHandler.GetIdFromToken(token);
            User user = await _userRepository.GetAsync(u => u.Id == userId);
            bool passwordValid = user.Password == passwordHasher(updateUserDTO.Password);
            if (!passwordValid)
                throw new ArgumentException("Girdiğiniz şifre yanlıştır.");

            user.Surname = updateUserDTO.Surname;
            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.Password = passwordHasher(updateUserDTO.NewPassword);
            _userRepository.Update(user);
        }

        private string passwordHasher(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashedValue = BitConverter.ToString(hashBytes).Replace("/", String.Empty);
                return hashedValue;
            }
        }
    }
}
