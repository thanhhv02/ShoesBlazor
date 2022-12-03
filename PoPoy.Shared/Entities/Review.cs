using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoPoy.Shared.Dto
{
    [Index(nameof(UserId))]
    public class Review
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } = 5;

        [Required(ErrorMessage = "Phải nhập tiêu đề.")]
        [MaxLength(50, ErrorMessage = "Tiêu đề không được quá 50 ký tự.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Phải nhập đánh giá。")]
        [MaxLength(1000, ErrorMessage = "Tiêu đề không được quá 1000 ký tự")]
        [Column(TypeName = "nvarchar(1000)")]
        public string ReviewText { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        //public virtual Product Product { get; set; }
        public User User { get; set; }
    }
}

