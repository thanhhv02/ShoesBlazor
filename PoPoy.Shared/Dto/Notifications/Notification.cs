using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        [MaxLength(250)]
        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        [Required]
        public string Message { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar(200)")]
        public string DataUrl { get; set; }

        [Column(TypeName = "nvarchar(1000)")]

        public string Data { get; set; }

        public bool IsRead { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }



    }
}
