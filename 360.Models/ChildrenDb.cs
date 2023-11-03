using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ChildrenDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ChildName { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
