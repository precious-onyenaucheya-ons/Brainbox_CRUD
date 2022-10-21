using Brainbox.Core.DTO;
using Brainbox.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brainbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service=service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int pageSize, int pageNumber)
        {
            var response =  _service.GetProductsByPaginationAsync(pageSize, pageNumber);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _service.GetProductByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDto)
        {
            var response = await _service.AddProductAsync(productDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            var response = await  _service.UpdateProductAsync(id,productDto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _service.DeleteProductAsync(id);
            return Ok(response);
        }

    }
}
