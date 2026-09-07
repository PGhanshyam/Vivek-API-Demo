using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.Coupon
{
    public class UpdateCouponRequestModel
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long CouponTypeId { get; set; }

        [Required]
        [MaxLength(20)]
        public string CouponCode { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
        public string? DiscountType { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal DiscountValue { get; set; }
        public bool IsActive { get; set; }
    }
}
