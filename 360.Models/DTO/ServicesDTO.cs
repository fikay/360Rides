using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class ServicesDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
       
        public string ServiceDescription { get; set; }
       
    }
}
