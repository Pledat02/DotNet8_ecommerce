using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.PaginatedList;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HomeService _service;

        public HomeController(HomeService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {   

                Account account = await _service.LoginAsync(username, password);
                var claims = new List<Claim>();
                if (account.type == 0)
                {
                     claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, account.User.email),
                        new Claim(ClaimTypes.Name, account.User.fullname),
                        new Claim("Phone", account.User.phone),
                        new Claim("Address", account.User.address),
                        new Claim(ClaimTypes.Role, "Customer"),
                        new Claim("IDUser", account.User.id_user+"")
                    };

                }
                else
                {
                     claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, account.Staff.email),
                        new Claim(ClaimTypes.Name, account.Staff.name_staff),
                        new Claim("IDUser", account.Staff.id_staff+""),
                     
                     };
                    foreach (var role in account.Staff.StaffRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Role.role_name));
                    }
                }

                var claimsIdentity = new ClaimsIdentity(claims, "login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.MessageLogin = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email, string address, string phone, string fullname)
        {
            try
            {
                Account acc = new Account
                {
                    username = username,
                    password = password,
                    type = 0
                };

                Account responseAcc = await _service.InsertAccount(acc);

                User user = new User
                {
                    fullname = fullname,
                    email = email,
                    address = address,
                    phone = phone,
                    id_account = responseAcc.id_account
                };

                await _service.CreateUserAsync(user);

                ViewBag.MessageLogin = "Đăng ký thành công, vui lòng đăng nhập để tiếp tục.";
                return RedirectToAction("Login", "Home");
            }
            catch (Exception)
            {
                ViewBag.MessageRegister = "Tên đăng nhập đã tồn tại";
                return View();
            }
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 12)
        {
            await PopulateCategoriesAsync();
            await PopulateVegetableAsync();
            await Populate4BestSellerAsync();
            await Populate6BestSellerAsync();
            var products = await _service.GetPagedProductsAsync(pageIndex, pageSize);
            return View(products);
        }

        public async Task<IActionResult> Shop(int pageIndex = 1, int pageSize = 12)
        {
            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateCategoriesAsync();
            await PopulateVegetableAsync();
            var products = await _service.GetPagedProductsAsync(pageIndex, pageSize);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> SearchShop(string keyword, string category, string price, int pageIndex = 1, int pageSize = 12)
        {
            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateCategoriesAsync();
            await PopulateVegetableAsync();
            decimal maxPriceDecimal = decimal.Parse(price);
            var products = await _service.SearchAsync(keyword, category, maxPriceDecimal);
            var pagedProducts = await PaginatedList<Product>.CreateAsync(products.AsQueryable(), pageIndex, pageSize);

            return PartialView("_ProductListShop", pagedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> SearchIndex(string category, int pageIndex = 1, int pageSize = 12)
        {
            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateCategoriesAsync();
            await PopulateVegetableAsync();
            var products = await _service.SearchAsync("", category, 0);
            var pagedProducts = await PaginatedList<Product>.CreateAsync(products.AsQueryable(), pageIndex, pageSize);

            return PartialView("_ProductListHome", pagedProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task PopulateCategoriesAsync()
        {
            ViewBag.Categories = await _service.GetCategoriesAsync();
        }

        public async Task PopulateVegetableAsync()
        {
            ViewBag.Vegetables = await _service.GetListProductVegetable();
        }

        public async Task Populate6BestSellerAsync()
        {
            ViewBag.BestSeller6 = await _service.GetTopProducts(6);
        }

        public async Task Populate4BestSellerAsync()
        {
            ViewBag.BestSeller4 = await _service.GetTopProducts(4);
        }

        public async Task PopulateCategoryWithQuantityAsync()
        {
            ViewBag.CategoriesHashMap = await _service.GetCategoryWithQuantity();
        }
    }
}
