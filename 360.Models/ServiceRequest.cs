using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        public int ServiceId { get; set; }
        [ValidateNever]
        [ForeignKey("ServiceId")]

        public ServicesModel Service { get; set; }

        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name ="Children Number")]
        public int childrenNumber { get; set; }
        [Required]
        [Display(Name ="Child Name")]
        public List<ChildrenName> childrenNames { get; set; }
        [Required]
        [Display(Name = "PickUp Location")]
        public string PickUpLocation { get; set; }
        [Required]
        [Display(Name = "DropOff Location")]
        public string DropOffLocation { get; set; }
        [Required]

        [Display(Name = "Start Date / Pickup Time")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date / Drop Off Time")]
        public DateTime EndDate { get; set;}

        [ValidateNever]
        [Display(Name = "Additional Comments")]
      
        public string additionalComments { get; set; }
        public double price { get; set; }
    }
}
