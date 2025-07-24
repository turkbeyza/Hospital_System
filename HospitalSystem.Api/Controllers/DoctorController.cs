using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Api.Data;
using HospitalSystem.Api.Models;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DoctorsController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _context.doctors
                .Include(d => d.user)
                .ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            var doctor = await _context.doctors
                .Include(d => d.user)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid();
            _context.doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, Doctor updatedDoctor)
        {
            if (id != updatedDoctor.Id)
                return BadRequest();

            var doctor = await _context.doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            doctor.UserId = updatedDoctor.UserId;
            doctor.Specialization = updatedDoctor.Specialization;

            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _context.doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            _context.doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
