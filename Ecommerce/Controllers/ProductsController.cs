using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.Data;
using Ecommerce.Services;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetOneAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateVegetableAsync();
            await PopulateRelatedProductsAsync(product.id_category);

            var user = GetLoggedInUser();
            if (user != null)
            {
                ViewBag.account = user;
            }

            return View(product);
        }

        // Comment
        public async Task<IActionResult> PostComment(string rating, string review, string id_product)
        {
            int idProduct = int.Parse(id_product);

            var product = await _service.GetOneAsync(idProduct);

            // Prepare data
            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateVegetableAsync();
            await PopulateRelatedProductsAsync(product.id_category);

          
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    message = "Vui lòng đăng nhập để thực hiện chức năng bình luận"
                });
            }
            if (!User.IsInRole("Customer"))
            {
                return Json(new
                {
                    message = "Chức năng này chỉ được thực hiện bởi khách hàng "
                });
            }
            int rate = int.Parse(rating);
            int id_user = int.Parse(User.FindFirst("IDUser").Value);
            // Post
            Comment comment = new Comment
            {
                text = review,
                id_product = idProduct,
                id_user = id_user,
                created_date = DateTime.Now,
                rating = rate,
            };
            Comment result = await _service.PostComment(comment);

            return Json(new
            {
                isPosted = true,
                comment = new
                {
                    text = result.text,
                    rating = result.rating,
                    created_date = result.created_date.ToString("dd-MM-yyyy HH:mm:ss tt"),
                    name_user = result.User.fullname,
                }
            });
        }

        public async Task<IActionResult> CheckIsSold(string id_product)
        {
            int idProduct = int.Parse(id_product);
            var product = await _service.GetOneAsync(idProduct);

            // Prepare data
            await PopulateCategoryWithQuantityAsync();
            await Populate4BestSellerAsync();
            await PopulateVegetableAsync();
            await PopulateRelatedProductsAsync(product.id_category);

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    message = "Vui lòng đăng nhập để thực hiện chức năng bình luận"
                });
            }
            if (!User.IsInRole("Customer"))
            {
                return Json(new
                {
                    message = "Chức năng này chỉ được thực hiện bởi khách hàng "
                });
            }

            bool isSold = await _service.CheckIsSoldAsync(idProduct, 
                int.Parse(User.FindFirst("IDUser").Value));
            if (isSold)
            {
                return Json(new { isSold = true });
            }
            else
            {
                return Json(new { isSold = false, message = "Vui lòng mua sản phẩm để thực hiện đánh giá" });
            }
        }

        // Helper method to get the logged-in user from the authentication cookie
        private Account GetLoggedInUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            {
                var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
                var fullname = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var phone = claimsIdentity.FindFirst("Phone")?.Value;

                return new Account
                {
                    User = new User
                    {
                        email = email,
                        fullname = fullname,
                        phone = phone
                    }
                };
            }

            return null;
        }

        // Other methods unchanged...

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesAndSuppliersAsync();
            return View(new Product());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,quantity_in_stock,price,description,url_image,id_category,id_supplier")] Product product)
        {
            await _service.InsertAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetOneAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await PopulateCategoriesAndSuppliersAsync(product.id_category, product.id_supplier);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_product,name,quantity_in_stock,price,description,url_image,id_category,id_supplier")] Product product)
        {
            if (id != product.id_product)
            {
                return NotFound();
            }

            await _service.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetOneAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool ProductExists(int id)
        {
            return _service.IsExists(id);
        }

        private async Task PopulateCategoriesAndSuppliersAsync(int? selectedCategoryId = null, int? selectedSupplierId = null)
        {
            var categories = await _service.GetCategoriesAsync();
            var suppliers = await _service.GetSuppliersAsync();

            ViewBag.Categories = new SelectList(categories, "id_category", "name", selectedCategoryId);
            ViewBag.Suppliers = new SelectList(suppliers, "id_supplier", "name_company", selectedSupplierId);
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

        public async Task PopulateRelatedProductsAsync(int id_category)
        {
            ViewBag.RelatedProducts = await _service.GetSampeCategoryProductAsync(id_category);
        }
    }
}
