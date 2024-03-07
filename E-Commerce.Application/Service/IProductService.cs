using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ViewResult;

namespace E_Commerce.Application.Service
{
    public interface IProductService
    {
        Task<ResultView<CreateOrUpdateProductDTO>> Delete(int Id);
        Task<ResultView<List<GetAllProductDTO>>> GetAll();
        Task<ResultView<GetAllProductDTO>> GetById(int Id);
        Task<ResultView<CreateOrUpdateProductDTO>> PostProduct(CreateOrUpdateProductDTO _productDto);
        Task<ResultView<CreateOrUpdateProductDTO>> UpdateOroduct(int OldId, CreateOrUpdateProductDTO _productDto);
    }
}