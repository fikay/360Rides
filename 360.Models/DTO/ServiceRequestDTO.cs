using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class ServiceRequestDTO
    {
        public int childrenNumber { get; set; }

        public int Id { get; set; }

        public int ServiceId { get; set; }
        [ValidateNever]
        public string ServiceName { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Children Name")]
        public List<string> childrenName { get; set; }

        [Display(Name = "PickUp Location")]
        public string PickUpLocation { get; set; }
        [Display(Name = "DropOff Location")]
        public string DropOffLocation { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Additional Comments")]
        public string additionalComments { get; set; }
        public double price { get; set; }

        //public List<int> childId { get; set; }
    }
}
