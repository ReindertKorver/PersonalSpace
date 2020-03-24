using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class DocumentsController : ControllerBase
    {
        private readonly UserContext _context;

        public DocumentsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocument()
        {
            return await _context.Document.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // POST: api/Documents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            if (!string.IsNullOrEmpty(document.Base64))
            {
                if (document.UserId == 0)
                {
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(document.Url))
                {
                    return BadRequest();
                }
                var user = _context.User.FirstOrDefault(u => u.Id == document.UserId);
                var path = "Documents/" + user.Username+ "/";
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                System.IO.File.WriteAllBytes(path + document.FileName + document.FileExtension, System.Convert.FromBase64String(document.Base64));
                document.Url = path + document.FileName + document.FileExtension;
                
                _context.Document.Add(document);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDocument", new { id = document.Id }, document);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Document.Remove(document);
            await _context.SaveChangesAsync();

            return document;
        }

        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.Id == id);
        }
    }
}