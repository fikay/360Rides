using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class AdminRequestDTO
    {
        public int id {  get; set; }
        public string orderId { get; set; }
        public string orderStatus { get; set; }

        public ReceivedRequestDetails ReceivedRequestDetails { get; set; }

        public string RequesterName { get; set; }
        public string RequesterEmail { get; set;}
    }
}
