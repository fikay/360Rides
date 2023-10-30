using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
   public class ReceivedRequestHeader
    {

        [Key]
        public int Id { get; set; }

        public string Userid { get; set; }
        [ForeignKey("Userid")]
        public ApplicationUser user { get; set; }

        public string OrderId { get; set; }

        public string OrderStatus { get; set; }

        public int requestDetails {  get; set; }
        [ForeignKey("requestDetails")]
        public ReceivedRequestDetails details { get; set; }
    }
}
