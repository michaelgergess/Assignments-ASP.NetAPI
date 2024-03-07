using AutoMapper;
using E_Commerce.Application.Contract;
using E_Commerce.DTOs.Role;
using E_Commerce.DTOs.ViewResult;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IMapper _Mapper;
        private readonly IUserRoleRepository _UserRoleRepository;

        public UserRoleService(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _Mapper = mapper;
            _UserRoleRepository = userRoleRepository;
        }
        public async Task<ResultView<AddRoleForUserDto>> CreateAsync(AddRoleForUserDto addRoleForUser)
        {
            bool test = _UserRoleRepository.GetAllAsync().Result.Any(
                u => u.UserId == addRoleForUser.UserId && u.RoleId == addRoleForUser.RoleId
            );
            if (test)
            {
                return new ResultView<AddRoleForUserDto> { Entity = null, Message = "ٌRole Is Exit OR Data Invaild", IsSuccess = false };
            }

            var RoleForUser = new UserRole()
            {
                RoleId = addRoleForUser.RoleId,
                UserId = addRoleForUser.UserId
            };
            await _UserRoleRepository.CreateAsync(RoleForUser);
            await _UserRoleRepository.SaveChangesAsync();

            return new ResultView<AddRoleForUserDto> { Entity = addRoleForUser, Message = "Successed", IsSuccess = true };

        }



    }
}
