using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Helper;
using Ecommerce.Services;
using System.Security.Claims;
using Azure.Core;
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
        public IActionResult Chackout()
        {
            ViewBag.Cart = Cart;
            ViewBag.Total = GetTotal();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
                var fullname = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var phone = claimsIdentity.FindFirst("Phone")?.Value;
                var address = claimsIdentity.FindFirst("Address")?.Value;

                // Assuming fullname contains more than two parts (firstname, middlename, lastname)
                string[] nameCpn = fullname?.Split(" ");
                var firstname = nameCpn?.FirstOrDefault();
                var lastname = nameCpn?.Length > 1 ? string.Join(" ", nameCpn.Skip(1)) : string.Empty;

                BillingDetailViewModel bill = new BillingDetailViewModel
                {
                    email = email,
                    firstname = firstname,
                    lastname = lastname,
                    address = address,
                    Phone = phone,
                    // You might want to add city, Country, PostalCode, etc. if available in claims
                };

                return View(bill);
            }

            return View(new BillingDetailViewModel{}); 
        }


        [HttpPost]
        public async Task<IActionResult> Chackout([Bind("firstname,lastname,address,city,Country,Phone,PostalCode,email,shipping,amount,payment_method")] BillingDetailViewModel bill)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(bill);
            }

            List<OrderDetail> list = new List<OrderDetail>();
            foreach (var product in Cart)
            {
                list.Add(new OrderDetail
                {
                    id_product = product.id_product,
                    quantity = product.quantity,
                    total_price = Convert.ToDecimal(product.amount),
                });
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            string? idUserClaim = null;
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                idUserClaim = claimsIdentity.FindFirst("IDUser")?.Value;
            }
            Order order = new Order
            {
                OrderDetails = list,
                order_time = DateTime.Now,
                status = "Đặt hàng thành công",
            };

            Bill billInsert = new Bill
            {
                Order = order,
                address = bill.address,
                amount = Convert.ToDecimal(bill.amount),
                firstname = bill.firstname,
                lastname = bill.lastname,
                CountryCode = bill.Country,
                PostalCode = bill.PostalCode,
                email = bill.email,
                Phone = bill.Phone,
                city = bill.city,
                payment_method = bill.payment_method,
                shipping = bill.shipping,
            };
            //
            HttpContext.Session.Set<Bill>(SettingKey.Bill_KEY, billInsert); 
                //  await _service.Chackout(billInsert);

            return RedirectToAction("SendToPaymen", "Payment");
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
