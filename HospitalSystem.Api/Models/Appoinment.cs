using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalSystem.Api.Models;
using YourProjectName.Models;

namespace HospitalSystem.Api.Models
{
    public class Appointment
    {
        [Key]
        public Guid id { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }

        [ForeignKey("Hospital")]
        public Guid hospitalId { get; set; }

        public DateTime appointmentDate { get; set; }

        public User user { get; set; }
        public Hospital hospital { get; set; }
        
        
    }
}