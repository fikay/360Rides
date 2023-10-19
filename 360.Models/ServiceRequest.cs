using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int childrenNumber { get; set; }
        [Required]
        public List<ChildrenNames> childrenNames { get; set; }
        [Required]
        public string PickUpLocation { get; set; }
        [Required]
        public string DropOffLocation { get; set; }
        [Required]

        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set;}

        public int price { get; set; }
    }
}
