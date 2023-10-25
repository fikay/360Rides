using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class ChildrenName
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string UserId {  get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
