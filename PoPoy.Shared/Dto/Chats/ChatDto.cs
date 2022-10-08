using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class ChatDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool? IsRead { get; set; } = false;
        public string Data { get; set; }

        public Guid? Sender { get; set; }
        public Guid? Receiver { get; set; }


    }

}
