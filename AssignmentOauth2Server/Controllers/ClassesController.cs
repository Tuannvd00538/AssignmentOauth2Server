﻿using System;
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
    public class ClassesController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public ClassesController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: _api/v1/Classes
        [HttpGet]
        public IEnumerable<Class> GetClass()
        {
            return _context.Class;
        }

        // GET:_api/v1/Classes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @class = await _context.Class.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return Ok(@class);
        }

        // PUT:_api/v1/Classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass([FromRoute] int id, [FromBody] Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @class.Id)
            {
                return BadRequest();
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: _api/v1/Classes
        [HttpPost]
        public async Task<IActionResult> PostClass([FromBody] Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Class.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        }

        // DELETE:_api/v1/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();

            return Ok(@class);
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.Id == id);
        }
    }
}