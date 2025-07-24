using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Api.Data;
using HospitalSystem.Api.Models;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Eğer JWT veya rol tabanlı yetkilendirme varsa:
    // [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public UserController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.users.ToListAsync();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();

            var user = await _context.users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Height = updatedUser.Height;
            user.Weight = updatedUser.Weight;
            user.Type = updatedUser.Type;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
