using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber {  get; set; }
        public string State {  get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

    }
}
