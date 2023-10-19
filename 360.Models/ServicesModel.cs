using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ServicesModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Service Name")]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name = "Service Description")]
        public string ServiceDescription { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        [ValidateNever]
        public string?CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Display(Name ="Price/KM")]
        public int priceperkm { get; set; }
    }
}
