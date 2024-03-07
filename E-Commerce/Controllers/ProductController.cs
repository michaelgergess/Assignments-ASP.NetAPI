using E_Commerce.Application.Service;
using E_Commerce.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }
        [HttpGet("{id:int}",Name ="GetOne")]
        public async Task< IActionResult> GetById(int id)
        {
            if (id > 0)
                if (ModelState.IsValid)
                {
                    return Ok( await _ProductService.GetById(id));
                }
            return BadRequest(ModelState);


        }
        [HttpGet]
        public async Task< IActionResult> GetAll()
        {

            
                return Ok( await _ProductService.GetAll());
            
           
        }
        [HttpPost]
        public async Task< IActionResult> Create(CreateOrUpdateProductDTO productDTO)
        {
            if (productDTO is not null)
            {
                if (ModelState.IsValid)
                {
                  //  string route = Url.Link("GetOne", new { id = productDTO.Id });

                    return Created("route", await _ProductService.PostProduct(productDTO));

                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,CreateOrUpdateProductDTO productDTO)
        {
            if (productDTO is not null)
            {
                if (ModelState.IsValid)
                {
                    string route = Url.Link("GetOne",new {id= id});
                    return Created("route", await _ProductService.UpdateOroduct(id,productDTO));
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
                if (ModelState.IsValid)
                {
                    return Ok(await _ProductService.Delete(id));
                }
            return BadRequest(ModelState);
        }
    }
}
