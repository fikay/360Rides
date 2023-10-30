using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ReceivedRequestDetails
    {
        [Key]
        public int Id { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }

    }
}
