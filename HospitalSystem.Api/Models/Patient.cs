using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalSystem.Api.Models;

namespace YourProjectName.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid userId { get; set; }
        public string Location { get; set; }

        public User user { get; set; }
    }
}