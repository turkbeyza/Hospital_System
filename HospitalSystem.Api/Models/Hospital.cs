using System;
using System.ComponentModel.DataAnnotations;

namespace YourProjectName.Models
{
    public class Hospital
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}