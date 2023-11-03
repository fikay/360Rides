using _360.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.ViewModels
{
    public class OrderVM
    {
        public AdminRequestDetailsDTO ReceivedRequestDetails { get; set; }
        public AdminRequestDTO ReceivedRequestHeader { get; set; }
        public List<SelectListItem> selectListItems { get; set; }
    }
}
