using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServicecs _productServicecs;
        public ProductController(ProductServicecs productServicecs)
        {
           _productServicecs = productServicecs;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var product = await _productServicecs.GetOneAsync(id);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet] 
        public async  Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productServicecs.GetAllAsync();
                if (products == null)
                    return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Product product)
        {
            if (product == null)
                return BadRequest("Product cannot be null");
            try
            {

                var productVM = await _productServicecs.InsertAsync(product);
                if (productVM == null)
                    return StatusCode(500);
                return Ok(productVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if( product==null || id != product.id_product)
                return BadRequest();
            try
            {
                await _productServicecs.UpdateAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productServicecs.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
