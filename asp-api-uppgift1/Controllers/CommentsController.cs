using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_api_uppgift1.Data;
using asp_api_uppgift1.Models;
using asp_api_uppgift1.Services;
using asp_api_uppgift1.ViewModel;

namespace asp_api_uppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IIdentityService _identity;

        public CommentsController(SqlDbContext context, IIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }


        // GET: api/Comments
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Comment>> GetComments(int id)
        {
            var issues = _context.Comments.Where(i => i.IssueId == id);
            return new OkObjectResult(issues);
        }



        //// GET: api/Comments/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Comment>> GetComment(int id)
        //{
        //    var comment = await _context.Comments.FindAsync(id);

        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return comment;
        //}




        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }





        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<CreateCommentModel>> PostComment(CreateCommentModel commentModel)
        {
            if (await _identity.CreateCommentAsync(commentModel))
                return new OkResult();

            return new BadRequestResult();


            //_context.Comments.Add(comment);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }






        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
