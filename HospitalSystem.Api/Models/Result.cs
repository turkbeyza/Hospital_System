using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystem.Api.Models
{
    public class Result
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Appointment")]
        public Guid AppointmentId { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Appointment appointment { get; set; }
        
    }
}