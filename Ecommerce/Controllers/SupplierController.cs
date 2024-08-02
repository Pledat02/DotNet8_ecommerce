using Ecommerce.Data;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var suppliers = await _supplierService.GetAllAsync();
                return Ok(suppliers);
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
                var suppliers = await _supplierService.GetOneAsync(id);
                if (suppliers == null)
                    return NotFound();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Supplier supplier)
        {
            try
            {
                var suppliers = await _supplierService.InsertAsync(supplier);
                if (suppliers == null)
                    return StatusCode(500);
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Supplier supplier)
        {
            if (supplier == null || id != supplier.id_supplier)
                return BadRequest();

            try
            {
                await _supplierService.UpdateAsync(supplier);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _supplierService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
