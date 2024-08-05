using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsService _orderDetailsService;

        public OrderDetailsController(OrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var orderDetail = await _orderDetailsService.GetOneAsync(id);
                if (orderDetail == null)
                    return NotFound();
                return Ok(orderDetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orderDetails = await _orderDetailsService.GetAllAsync();
                if (orderDetails == null)
                    return NotFound();
                return Ok(orderDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(OrderDetail orderDetails)
        {
            if (orderDetails == null)
                return BadRequest("Order details cannot be null");
            try
            {
                var orderDetailsVM = await _orderDetailsService.InsertAsync(orderDetails);
                if (orderDetailsVM == null)
                    return StatusCode(500);
                return Ok(orderDetailsVM);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDetail orderDetails)
        {
            if (orderDetails == null || id != orderDetails.id_order)
                return BadRequest();
            try
            {
                await _orderDetailsService.UpdateAsync(orderDetails);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderDetailsService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
    */
}
