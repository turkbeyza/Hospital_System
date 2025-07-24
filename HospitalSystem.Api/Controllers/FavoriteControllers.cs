using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;
using HospitalSystem.Api.Data;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class FavoriteController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public FavoriteController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _context.favorites
                .Include(f => f.patient)
                .Include(f => f.doctor)
                .ToListAsync();
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavorite(Guid id)
        {
            var favorite = await _context.favorites
                .Include(f => f.patient)
                .Include(f => f.doctor)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (favorite == null)
                return NotFound();

            return Ok(favorite);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavorite(Favorite favorite)
        {
            favorite.Id = Guid.NewGuid();
            _context.favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavorite), new { id = favorite.Id }, favorite);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavorite(Guid id, Favorite updatedFavorite)
        {
            if (id != updatedFavorite.Id)
                return BadRequest();

            var favorite = await _context.favorites.FindAsync(id);
            if (favorite == null)
                return NotFound();

            favorite.PatientId = updatedFavorite.PatientId;
            favorite.DoctorId = updatedFavorite.DoctorId;

            _context.Entry(favorite).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            var favorite = await _context.favorites.FindAsync(id);
            if (favorite == null)
                return NotFound();

            _context.favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
