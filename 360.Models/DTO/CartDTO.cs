using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
    }
}
