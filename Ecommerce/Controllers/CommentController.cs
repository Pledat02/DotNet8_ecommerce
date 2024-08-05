using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{   /*
    [Route("api/[controller]")]
     [ApiController]

     public class CommentController : ControllerBase
     {
         private readonly CommentService _commentService;

         public CommentController(CommentService commentService)
         {
             _commentService = commentService;
         }

         [HttpGet("{id}")]
         public async Task<IActionResult> GetOne(int id)
         {
             try
             {
                 var comment = await _commentService.GetOneAsync(id);
                 if (comment == null)
                     return NotFound();
                 return Ok(comment);
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
                 var comments = await _commentService.GetAllAsync();
                 if (comments == null)
                     return NotFound();
                 return Ok(comments);
             }
             catch (Exception)
             {
                 return BadRequest();
             }
         }

         [HttpPost]
         public async Task<IActionResult> Insert(Comment comment)
         {
             if (comment == null)
                 return BadRequest("Comment cannot be null");
             try
             {
                 var commentVM = await _commentService.InsertAsync(comment);
                 if (commentVM == null)
                     return StatusCode(500);
                 return Ok(commentVM);
             }
             catch (Exception)
             {
                 return BadRequest();
             }
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> Update(int id, Comment comment)
         {
             if (comment == null || id != comment.id_comment)
                 return BadRequest();
             try
             {
                 await _commentService.UpdateAsync(comment);
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
                 await _commentService.DeleteAsync(id);
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
