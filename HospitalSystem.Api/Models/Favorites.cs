using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalSystem.Api.Models;

namespace YourProjectName.Models
{
    public class Favorite
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }

        [ForeignKey("Doctor")]
        public Guid DoctorId { get; set; }

        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
        
    }
}