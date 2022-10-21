using AutoMapper;
using Brainbox.Core.Interfaces;
using Brainbox.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Brainbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IProductService productService, ICartService cartService,IMapper mapper)
        {
            _orderService = orderService;
            _productService = productService;
            _cartService = cartService;
            _mapper = mapper;
        }

        [Route("api/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> CompleteOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _orderService.StoreOrderAsync(_cartService.GetCartItems(), userId, userEmail);
            await _cartService.ClearCartAsync();
            return Ok();
        }

        [Route("api/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = _productService.GetProductByIdAsync(id);
            var result = _mapper.Map<Product>(product.Result.Data);
            if (result != null)
            {
                _cartService.AddItemToCart(result);
                return Ok();
            }
            return NoContent();
        }

        [Route("api/[controller]/[action]")]
        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var product = _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _cartService.RemoveItemFromCart(product.Result.Data);
                return Ok();
            }
            return NoContent();

        }

        [Route("api/[controller]/[action]")]
        [HttpGet]
        public IActionResult GetCartItems()
        {
            var response = _cartService.GetCartItems();
            return Ok(response);
        }

        [Route("api/[controller]/[action]")]
        [HttpGet]
        public IActionResult GetCartTotal()
        {
            var response = _cartService.CartTotal();
            return Ok(response);
        }
    }
}
