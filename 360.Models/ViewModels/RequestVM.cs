using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.ViewModels
{
    public class RequestVM
    {
        public ServiceRequest Request { get; set; }
        public IEnumerable<ChildrenName> ChildrenNames { get; set; }
    }
}
