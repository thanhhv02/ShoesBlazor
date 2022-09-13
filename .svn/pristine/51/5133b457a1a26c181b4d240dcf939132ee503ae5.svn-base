using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class Role : IdentityRole<Guid>
    {
        [MaxLength(250)]
        [Required]
        public string Description { get; set; }
    }
}
