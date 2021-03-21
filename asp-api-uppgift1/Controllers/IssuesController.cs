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
using Microsoft.AspNetCore.Authorization;

namespace asp_api_uppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IIdentityService _identity;

        public IssuesController(SqlDbContext context, IIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }


        // GET: api/Issues  //all issues of a customer
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Issue>> GetIssues(int id)
        {

            var issues = _context.Issues.Where(i => i.CustomerId == id);
            return new OkObjectResult(issues);
        }


        [HttpGet("completed/{id}")]
        public ActionResult<IEnumerable<Issue>> GetCompletedIssues(int id)
        {

            var issues = _context.Issues.Where(i => i.CustomerId == id).Where(i => i.Status == Status.Completed);
            return new OkObjectResult(issues);
        }


        [HttpGet("inactive/{id}")]
        public ActionResult<IEnumerable<Issue>> GetInactiveIssues(int id)
        {

            var issues = _context.Issues.Where(i => i.CustomerId == id).Where(i => i.Status == Status.Inactive);
            return new OkObjectResult(issues);
        }

        [HttpGet("started/{id}")]
        public ActionResult<IEnumerable<Issue>> GetStartedIssues(int id)
        {

            var issues = _context.Issues.Where(i => i.CustomerId == id).Where(i => i.Status == Status.Started);
            return new OkObjectResult(issues);
        }



        //// GET: api/Issues/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Issue>> GetIssue(int id)
        //{
        //    var issue = await _context.Issues.FindAsync(id);

        //    if (issue == null)
        //    {
        //        return NotFound();
        //    }

        //    return issue;
        //}


        // PUT: api/Issues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutIssue(int id, Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(id))
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





        // POST: api/Issues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<IActionResult> CreateIssueAsync([FromBody] CreateIssueModel issueModel)
        {
            if (await _identity.CreateIssueAsync(issueModel))
                return new OkResult();

            return new BadRequestResult();
        }




        // DELETE: api/Issues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
