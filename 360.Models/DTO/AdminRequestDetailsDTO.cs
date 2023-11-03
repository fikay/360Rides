using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class AdminRequestDetailsDTO
    {
        public int ServiceId { get; set; }
       

        public ServicesModel Service { get; set; }

        
        [Display(Name = "Children Number")]
        public int childrenNumber { get; set; }
       
        [Display(Name = "Child Name")]
        public List<ChildrenName> childrenNames { get; set; }
        
        [Display(Name = "PickUp Location")]
        public string PickUpLocation { get; set; }
        
        [Display(Name = "DropOff Location")]
        public string DropOffLocation { get; set; }
        

        [Display(Name = "Start Date / Pickup Time")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date / Drop Off Time")]
        public DateTime EndDate { get; set; }

        [ValidateNever]
        [Display(Name = "Additional Comments")]

        public string additionalComments { get; set; }
        public double price { get; set; }
    }
}
