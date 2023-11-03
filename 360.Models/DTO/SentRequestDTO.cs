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
    public class SentRequestDTO
    {
        public int ServiceId { get; set; }
        

        public int childrenNumber { get; set; }
       
        public List<ChildrenName> childrenNames { get; set; }
        
        public string PickUpLocation { get; set; }
       
        public string DropOffLocation { get; set; }
        
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }

        public string additionalComments { get; set; }
        public double price { get; set; }
    }
}
