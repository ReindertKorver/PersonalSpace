using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly UserContext _context;

        public TasksController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PSTask>>> GetPSTask()
        {
            return await _context.PSTask.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PSTask>> GetPSTask(int id)
        {
            var pSTask = await _context.PSTask.FindAsync(id);

            if (pSTask == null)
            {
                return NotFound();
            }

            return pSTask;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPSTask(int id, PSTask pSTask)
        {
            if (id != pSTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(pSTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PSTaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PSTask>> PostPSTask(PSTask pSTask)
        {
            if (pSTask.User != null && pSTask.User.Id != 0)
            {
                return BadRequest();
            }
            _context.PSTask.Add(pSTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPSTask", new { id = pSTask.Id }, pSTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PSTask>> DeletePSTask(int id)
        {
            var pSTask = await _context.PSTask.FindAsync(id);
            if (pSTask == null)
            {
                return NotFound();
            }

            _context.PSTask.Remove(pSTask);
            await _context.SaveChangesAsync();

            return pSTask;
        }

        private bool PSTaskExists(int id)
        {
            return _context.PSTask.Any(e => e.Id == id);
        }
    }
}