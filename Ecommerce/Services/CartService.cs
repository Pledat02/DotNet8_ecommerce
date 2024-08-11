using Ecommerce.Helper;
using Ecommerce.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Ecommerce.Services
{
    public interface ICartService
    {
        Task RestoreStockAsync();
    }

    public class CartService : ICartService
    {
        private readonly ProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(ProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RestoreStockAsync()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.Get<List<CartItemViewModel>>(SettingKey.CART_KEY) ?? new List<CartItemViewModel>();

            foreach (var item in cart)
            {
                var product = await _productService.GetOneAsync(item.id_product);
                if (product != null)
                {
                    product.quantity_in_stock += item.quantity;
                    await _productService.UpdateAsync(product);
                }
            }
        }
    }
}
