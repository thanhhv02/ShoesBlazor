using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoPoy.Shared.Dto
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public Product Product { get; set; }
        public int Type { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateExpired { get; set; }
        public double Reduction { get; set; }
        [NotMapped]
        public bool IsValid =>
            Type == (int) CouponType.Date ? DateTime.Now < DateExpired : Quantity > 0;
        public bool Deleted { get; set; }
        public ICollection<OrderCoupon> OrderCoupons { get; set; }
    }
}