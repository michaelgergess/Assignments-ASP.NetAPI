using E_Commerce.DTOs.Role;
using E_Commerce.DTOs.ViewResult;

namespace E_Commerce.Application.Service
{
    public interface IRoleService
    {
        Task<ResultView<AddOrEditRoleDto>> CreateAsync(AddOrEditRoleDto roleDto);
    }
}