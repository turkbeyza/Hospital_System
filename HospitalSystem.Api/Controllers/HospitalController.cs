using HospitalSystem.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class HospitalController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public HospitalController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitals()
        {
            var hospitals = await _context.hospitals.ToListAsync();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHospital(Guid id)
        {
            var hospital = await _context.hospitals.FindAsync(id);
            if (hospital == null)
                return NotFound();

            return Ok(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospital(Hospital hospital)
        {
            hospital.Id = Guid.NewGuid();
            _context.hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHospital), new { id = hospital.Id }, hospital);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHospital(Guid id, Hospital updatedHospital)
        {
            if (id != updatedHospital.Id)
                return BadRequest();

            var hospital = await _context.hospitals.FindAsync(id);
            if (hospital == null)
                return NotFound();

            hospital.Name = updatedHospital.Name;
            hospital.Address = updatedHospital.Address;
            hospital.Phone = updatedHospital.Phone;

            _context.Entry(hospital).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            var hospital = await _context.hospitals.FindAsync(id);
            if (hospital == null)
                return NotFound();

            _context.hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
