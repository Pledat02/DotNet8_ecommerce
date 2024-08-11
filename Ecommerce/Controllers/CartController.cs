using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Helper;
using Ecommerce.Services;
namespace Ecommerce.Controllers
{
    public class CartController : BaseController
    {   
        private readonly ProductService _service;

        public CartController (ProductService service)
        {
            _service = service;
        }
     
        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(SettingKey.CART_KEY) ??
            new List<CartItemViewModel>(); 

        public IActionResult Index()
        {
            ViewBag.Total = GetTotal();
            return View(Cart);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart (int id_product, int quantity)
        {
            var TmpCart = Cart;
            var item = TmpCart.SingleOrDefault(p => p.id_product == id_product);
            var product = await _service.GetOneAsync(id_product);
            if (item == null)
            {
              
                if (product == null)
                {
                    return NotFound();
                }
                TmpCart.Add(new CartItemViewModel
                {
                    id_product = product.id_product,
                    quantity = quantity,
                    name = product.name,
                    url_image = product.url_image,
                    price = (double) product.price,
                });
               
            }
            else
            {
                item.quantity = quantity + item.quantity;
            }


            if (await _service.CheckStocking(id_product) == false)
            {
                ViewBag.message = "Mặt hàng này đã được bán hết";
                return RedirectToAction("Details", "Products", new { id = id_product });
            }

            HttpContext.Session.Set(SettingKey.CART_KEY, TmpCart);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id_product)
        {
            bool result = false ;
            var TmpCart = Cart;
            var item = TmpCart.SingleOrDefault(p => p.id_product == id_product);
            if(item == null)
            {
                return BadRequest("Sản phẩm không tồn tại trong giỏ hàng");
            }
            else
            {

                TmpCart.Remove(item);
                result =true;
            }
            HttpContext.Session.Set(SettingKey.CART_KEY, TmpCart);
            return Json(new { success = true, total = GetTotal()});
        }

        [HttpPost]
        public async Task<IActionResult> Subtract(int id_product)
        {
            var TmpCart = Cart;
            var item = TmpCart.SingleOrDefault(p => p.id_product == id_product);
            if (item == null)
            {
                return BadRequest("Sản phẩm không tồn tại trong giỏ hàng");
            }
            else
            {
               var product = await _service.GetOneAsync(item.id_product);
                if (product == null)
                {
                    return BadRequest("Sản phẩm không tồn tại trong Cơ sở dữ liệu");
                }
            
                item.quantity -= 1;
            }
            HttpContext.Session.Set(SettingKey.CART_KEY, TmpCart);
            return Json(new { quantity = item.quantity, amount = item.amount, total = GetTotal() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id_product)
        {
            var TmpCart = Cart;
            var item = TmpCart.SingleOrDefault(p => p.id_product == id_product);
            if (item == null)
            {
                return BadRequest("Sản phẩm không tồn tại trong giỏ hàng");
            }
            else
            {
                var product = await _service.GetOneAsync(item.id_product);
                if (product == null)
                {
                    return BadRequest("Sản phẩm không tồn tại trong Cơ sở dữ liệu");
                }
                if (await _service.CheckStocking(id_product) == false)
                {
                    return Json(new { IsStocking = false });
                }
                item.quantity += 1;
            }
            HttpContext.Session.Set(SettingKey.CART_KEY, TmpCart);
            return Json(new { quantity = item.quantity , amount = item.amount, total = GetTotal() });
        }

        private double GetTotal()
        {
            double total = 0;
            foreach (var item in Cart)
            {
                total += item.amount;
            }
          return total;
        }
    }
  
}
