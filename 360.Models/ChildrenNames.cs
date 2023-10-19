using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace _360.Models
{
    public class ChildrenNames
    {
        [Key]
        public int Id { get; set; }

        public string UserId {  get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
