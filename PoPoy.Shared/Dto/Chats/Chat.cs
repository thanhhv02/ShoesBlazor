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
    public class Chat 
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(Max)")]
        public string Message { get; set; }
                                      
        public bool? IsRead { get; set; }
        public string data { get; set; }

        public DateTime Created { get; set; }  

        public Guid? SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        public Guid? ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
    }

}
