using AutoMapper;
using E_Commerce.Application.Contract;
using E_Commerce.DTOs.User;
using E_Commerce.DTOs.ViewResult;
using E_Commerce.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _Mapper;
        private readonly IUserRepository _UserRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _Mapper = mapper;
            _UserRepository = userRepository;
        }
        public async Task<ResultView<AddOrEditUserDto>> CreateAsync(AddOrEditUserDto userDto)
        {
            var OldUser = await _UserRepository.GetByIdAsync(userDto.Id);
            if (userDto == null || OldUser != null)
            {
                return new ResultView<AddOrEditUserDto> { Entity = null, Message = "User Is Exit OR Data Invaild", IsSuccess = false };
            }
            var Password = HashTable.HashPassword(userDto.Password);
            var User = new User()
            {
                Email = userDto.Email
                ,
                Name = userDto.Name
                ,
                Password = Password
            };
            await _UserRepository.CreateAsync(User);
            await _UserRepository.SaveChangesAsync();

            return new ResultView<AddOrEditUserDto> { Entity = userDto, Message = "Successed", IsSuccess = true };

        }
        public async Task<ResultView<LoginDto>> LoginAsync(LoginDto userDto)
        {
            var OldUser = _UserRepository.GetAllAsync().Result.FirstOrDefault(u => u.Email == userDto.Email);
            if (OldUser == null)
            {
                return new ResultView<LoginDto> { Entity = null, Message = "Email not found", IsSuccess = false };
            }
            if (HashTable.VerifyPassword(OldUser.Password, userDto.Password))
            {
                
                return new ResultView<LoginDto> { Entity = userDto, Message = "enterd Successed", IsSuccess = true };

            }
            return new ResultView<LoginDto> { Entity = null, Message = "Email not found", IsSuccess = false };


        }
    }
}
