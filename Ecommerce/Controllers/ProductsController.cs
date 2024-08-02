using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Services;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
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

            return View(product);
        }

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
    }
}
