using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class Employees
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]

        public string LicenseType { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }

        public string Role { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set;}
    }
}
