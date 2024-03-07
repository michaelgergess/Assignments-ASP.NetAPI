using AutoMapper;
using E_Commerce.Application.Contract;
using E_Commerce.DTOs.Role;
using E_Commerce.DTOs.User;
using E_Commerce.DTOs.ViewResult;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _Mapper;
        private readonly IRoleRepository _RoleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            _Mapper = mapper;
            _RoleRepository = roleRepository;
        }
        public async Task<ResultView<AddOrEditRoleDto>> CreateAsync(AddOrEditRoleDto roleDto)
        {
            var OldUser = await _RoleRepository.GetByIdAsync(roleDto.Id);
            if (roleDto == null || OldUser != null)
            {
                return new ResultView<AddOrEditRoleDto> { Entity = null, Message = "ٌRole Is Exit OR Data Invaild", IsSuccess = false };
            }

            var Role = new Role()
            {
                Name = roleDto.Name,
                Description = roleDto.Description,
            };
            await _RoleRepository.CreateAsync(Role);
            await _RoleRepository.SaveChangesAsync();

            return new ResultView<AddOrEditRoleDto> { Entity = roleDto, Message = "Successed", IsSuccess = true };

        }
    }
}
