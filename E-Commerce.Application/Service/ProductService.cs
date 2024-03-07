using AutoMapper;
using E_Commerce.Application.Contract;
using E_Commerce.Application.Mapper;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ViewResult;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper _Mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper autoMapper)
        {
            _Mapper = autoMapper;
            _productRepository = productRepository;
        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> PostProduct(CreateOrUpdateProductDTO _productDto)
        {
            var OldProduct = _productRepository.GetAllAsync().Result.FirstOrDefault(p => p.Name == _productDto.Name);
            if (OldProduct == null)
            {
                var Product = _Mapper.Map<Product>(_productDto);
                var NewProduct = await _productRepository.CreateAsync(Product);
                await _productRepository.SaveChangesAsync();

                return new ResultView<CreateOrUpdateProductDTO> { Entity = _productDto, IsSuccess = true, Message = "Created Successfully" };
            }
            else
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "Product is Exist" };
            }
        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> UpdateOroduct(int OldId, CreateOrUpdateProductDTO _productDto)
        {
            if (OldId > 0)
            {
                var OldProduct = await _productRepository.GetByIdAsync(OldId);

                if (_productDto != null)
                {
                    OldProduct.Name = _productDto.Name;
                    OldProduct.SupplierId = _productDto.SupplierId;
                    OldProduct.Price = _productDto.Price;
                    await _productRepository.UpdateAsync(OldProduct);
                    await _productRepository.SaveChangesAsync();

                    return new ResultView<CreateOrUpdateProductDTO> { Entity = _productDto, IsSuccess = true, Message = "Created Successfully" };
                }

            }
            return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "Product is Exist" };
        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> Delete(int Id)
        {
            if (Id > 0)
            {
                var product = await _productRepository.GetByIdAsync(Id);
                if (product is not null)
                {
                    var Oldprd = await _productRepository.DeleteAsync(product);
                    await _productRepository.SaveChangesAsync();
                    var PrdDto = _Mapper.Map<CreateOrUpdateProductDTO>(Oldprd);
                    return new ResultView<CreateOrUpdateProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
                }
            }

            return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "We Cant Delete Because We Found Error Please Try again" };

        }
        public async Task<ResultView<GetAllProductDTO>> GetById(int Id)
        {
            if (Id > 0)
            {
                var product = (await _productRepository.GetAllAsync()).FirstOrDefault(p => p.Id == Id);
                if (product is not null)
                {
                    var productDto = _Mapper.Map<GetAllProductDTO>(product);
                    return new ResultView<GetAllProductDTO> { Entity = productDto, IsSuccess = true, Message = "Deleted Successfully" };
                }
            }

            return new ResultView<GetAllProductDTO> { Entity = null, IsSuccess = false, Message = "We cant find any product" };

        }

        public async Task<ResultView<List<GetAllProductDTO>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            if (products is not null && products.Any())
            {
                var productDto = _Mapper.Map<List<GetAllProductDTO>>(products);
                return new ResultView<List<GetAllProductDTO>> { Entity = productDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            return new ResultView<List<GetAllProductDTO>> { Entity = null, IsSuccess = false, Message = "We cant find any product" };

        }

    }
}
