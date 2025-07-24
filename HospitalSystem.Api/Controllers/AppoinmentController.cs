using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystem.Api.Data;
using HospitalSystem.Api.Models;

namespace HospitalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class AppointmentController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public AppointmentController(HospitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _context.appointments
                .Include(a => a.user)
                .Include(a => a.hospital)
                .ToListAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(Guid id)
        {
            var appointment = await _context.appointments
                .Include(a => a.user)
                .Include(a => a.hospital)
                .FirstOrDefaultAsync(a => a.id == id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            appointment.id = Guid.NewGuid();
            _context.appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, Appointment updatedAppointment)
        {
            if (id != updatedAppointment.id)
                return BadRequest();

            var appointment = await _context.appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.userId = updatedAppointment.userId;
            appointment.hospitalId = updatedAppointment.hospitalId;
            appointment.appointmentDate = updatedAppointment.appointmentDate;

            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _context.appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
