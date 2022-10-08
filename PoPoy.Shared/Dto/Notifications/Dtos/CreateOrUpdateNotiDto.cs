using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class CreateOrUpdateNotiDto
    {


        [MaxLength(250)]
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        [Required]
        public string Message { get; set; }

        [MaxLength(500)]
        public string DataUrl { get; set; }


        public string Data { get; set; }

        public Guid? UserId { get; set; }

    
    }
}
