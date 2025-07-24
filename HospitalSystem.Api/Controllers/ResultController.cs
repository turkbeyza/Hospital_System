using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Api.Data;
using HospitalSystem.Api.Models;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class ResultController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public ResultController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.results
                .Include(r => r.appointment)
                .ToListAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult(Guid id)
        {
            var result = await _context.results
                .Include(r => r.appointment)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult(Result result)
        {
            result.Id = Guid.NewGuid();
            result.CreatedAt = DateTime.Now;

            _context.results.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResult), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(Guid id, Result updatedResult)
        {
            if (id != updatedResult.Id)
                return BadRequest();

            var result = await _context.results.FindAsync(id);
            if (result == null)
                return NotFound();

            result.AppointmentId = updatedResult.AppointmentId;
            result.FileName = updatedResult.FileName;
            result.FilePath = updatedResult.FilePath;
            // CreatedAt genellikle update edilmez.

            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(Guid id)
        {
            var result = await _context.results.FindAsync(id);
            if (result == null)
                return NotFound();

            _context.results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
