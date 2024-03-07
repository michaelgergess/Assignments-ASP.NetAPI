using AutoMapper;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.Role;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrUpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetAllProductDTO, Product>().ReverseMap();
            CreateMap<AddOrEditRoleDto, Role>().ReverseMap();
            CreateMap<AddRoleForUserDto, User>().ReverseMap();
            CreateMap<AddRoleForUserDto, UserRole>().ReverseMap();

        }
    }
}
