using Ecommerce.Data;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var categories = await _categoryService.GetOneAsync(id);
                if (categories == null)
                    return NotFound();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Category category)
        {
            try
            {
                var categories = await _categoryService.InsertAsync(category);
                if (categories == null)
                    return StatusCode(500);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {   
            if (category == null || id != category.id_category)
                return BadRequest();
          
            try
            {
                await _categoryService.UpdateAsync(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


    }
}
