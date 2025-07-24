using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;
using HospitalSystem.Api.Data;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class PatientController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PatientController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.patients
                .Include(p => p.user)
                .ToListAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await _context.patients
                .Include(p => p.user)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            _context.patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, Patient updatedPatient)
        {
            if (id != updatedPatient.Id)
                return BadRequest();

            var patient = await _context.patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            patient.userId = updatedPatient.userId;
            patient.Location = updatedPatient.Location;

            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var patient = await _context.patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            _context.patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
