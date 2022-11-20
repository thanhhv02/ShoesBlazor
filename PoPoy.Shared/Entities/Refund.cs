using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Entities
{
    public class Refund
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public DateTime DateRefunded { get; set; }
    }
}
