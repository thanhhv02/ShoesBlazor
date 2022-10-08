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

        [Column("nvarchar(1000)")]
        public string Message { get; set; }
                                      
        public bool? IsRead { get; set; }
        public string Data { get; set; }

        public DateTime Created { get; set; }  

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
