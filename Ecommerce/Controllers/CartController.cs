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
        private readonly UserService _userService;

        public CartController (ProductService service, UserService userService)
        {
            _service = service;
            _userService = userService;
        }
     
        public List<CartItemViewModel> Cart => HttpContext.Session.Get<List<CartItemViewModel>>(SettingKey.CART_KEY) ??
            new List<CartItemViewModel>(); 

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                if(User.IsInRole("Customer")){
                    var id = claimsIdentity.FindFirst("IDUser")?.Value;
                    User user = await _userService.GetOneAsync(int.Parse(id));
                    if (user.Voucher_Users != null)
                    {
                        ViewBag.Vouchers = user.Voucher_Users;
                    }
                }
            }
            ViewBag.Total = GetTotal();

            return View(Cart);
        }
        public async Task<IActionResult> Chackout(string vouchercode)
        {
            ViewBag.Cart = Cart;
            decimal total = GetTotal(); 

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity?.IsAuthenticated == true)
            {
                var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
                var fullname = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var phone = claimsIdentity.FindFirst("Phone")?.Value;
                var address = claimsIdentity.FindFirst("Address")?.Value;

                var nameParts = fullname?.Split(" ");
                var firstname = nameParts?.FirstOrDefault();
                var lastname = nameParts?.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

                var iduser = claimsIdentity.FindFirst("IDUser")?.Value;
                Voucher_User voucher = null;
                decimal discount = 0;

                if (!string.IsNullOrEmpty(vouchercode))
                {
                    voucher = await _userService.GetOneVouchersAsync(int.Parse(vouchercode));
                    decimal percentDiscount = voucher?.Voucher.percent_discount ?? 0;
                    discount = percentDiscount * total; // Ensure this is calculated with decimal precision
                    HttpContext.Session.Set<Voucher_User>(SettingKey.VOUCHER_KEY, voucher);
                }

                var bill = new BillingDetailViewModel
                {
                    email = email,
                    firstname = firstname,
                    lastname = lastname,
                    address = address,
                    Phone = phone,
                    amount = total - discount // Ensure this subtraction is with decimal precision
                };

                return View(bill);
            }

            return View(new BillingDetailViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Chackout([Bind("firstname,lastname,address,city,Country,Phone,PostalCode,email,shipping,amount,payment_method")] BillingDetailViewModel bill)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(bill);
            }
            Voucher_User voucher_User = HttpContext.Session.Get<Voucher_User>(SettingKey.VOUCHER_KEY);
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
                Voucher_User =  new Voucher_User
                {
                    id_user = int.Parse(idUserClaim),
                }
            };
            if(voucher_User != null)
            {
                order.Voucher_User.id_voucher = voucher_User.Voucher.id_voucher;
            }

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
            HttpContext.Session.Set<List<CartItemViewModel>>(SettingKey.CART_KEY, null);
        
            if (voucher_User != null)
            {
                await _userService.SetStateVoucher(voucher_User, 1);
            }
            await _service.Chackout(billInsert);

            return RedirectToAction("Index", "Home");
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

        private decimal GetTotal()
        {
            return Cart.Sum(item => (decimal)item.amount);
        }
    }

}
