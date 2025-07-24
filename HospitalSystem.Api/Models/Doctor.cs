using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalSystem.Api.Models;

namespace HospitalSystem.Api.Models
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string Specialization { get; set; }

        public User user { get; set; }
    }
}