using E_Commerce.DTOs.Role;
using E_Commerce.DTOs.ViewResult;

namespace E_Commerce.Application.Service
{
    public interface IUserRoleService
    {
        Task<ResultView<AddRoleForUserDto>> CreateAsync(AddRoleForUserDto addRoleForUser);
    }
}