using _360.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.ViewModels
{
    public class EmployeeVM
    {
       public EmployeeDTO employeeDTO { get; set; }
        [ValidateNever]
       public List<SelectListItem> roles { get; set; }
    }
}
