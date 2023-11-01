using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class ServicesAdminDTO
    {
        
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
       
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
       
        public int priceperkm { get; set; }
    }
}
