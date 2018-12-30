using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;

namespace AssignmentOauth2Server.Controllers
{
    [Route("_api/v1/[controller]")]
    [ApiController]
    public class Classes_StudentController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public Classes_StudentController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: api/Classes_Student
        [HttpGet]
        public IEnumerable<Classes> GetClasses()
        {
            return _context.Classes;
        }

        // GET: api/Classes_Student/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClasses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classes = await _context.Classes.FindAsync(id);

            if (classes == null)
            {
                return NotFound();
            }

            return Ok(classes);
        }

        // PUT: api/Classes_Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasses([FromRoute] int id, [FromBody] Classes classes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classes.Id)
            {
                return BadRequest();
            }

            _context.Entry(classes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassesExists(id))
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

        // POST: api/Classes_Student
        [HttpPost]
        public async Task<IActionResult> PostClasses([FromBody] Classes classes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Classes.Add(classes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasses", new { id = classes.Id }, classes);
        }

        // DELETE: api/Classes_Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();

            return Ok(classes);
        }

        private bool ClassesExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}