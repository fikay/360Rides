using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]

        public string LicenseType { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
