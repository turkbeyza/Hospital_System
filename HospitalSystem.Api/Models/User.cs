using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string Type { get; set; } // "Admin", "Doctor", "Patient"
    }
}
