using E_Commerce.DTOs.User;
using E_Commerce.DTOs.ViewResult;

namespace E_Commerce.Application.Service
{
    public interface IUserService
    {
        Task<ResultView<AddOrEditUserDto>> CreateAsync(AddOrEditUserDto userDto);
        Task<ResultView<LoginDto>> LoginAsync(LoginDto userDto);
    }
}