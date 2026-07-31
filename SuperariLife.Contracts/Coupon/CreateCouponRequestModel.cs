using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperariLife.Contracts.Coupon
{
    public class CreateCouponRequestModel
    {
        [Required(ErrorMessage = "Coupon type is required.")]
        [Range(1, long.MaxValue)]
        public long CouponTypeId { get; set; }

        [Required(ErrorMessage = "Coupon code is required.")]
        [MaxLength(20)]
        public string CouponCode { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        public DateTime ExpiryDate { get; set; }

        [Range(
            0.01,
            double.MaxValue,
            ErrorMessage = "Discount value must be greater than zero."
        )]
        public decimal DiscountValue { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
