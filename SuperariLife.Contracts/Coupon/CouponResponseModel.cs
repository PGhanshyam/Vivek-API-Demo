using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.Coupon
{
    public class CouponResponseModel
    {
        public long CouponId { get; set; }

        public long CouponTypeId { get; set; }

        public string CouponTypeName { get; set; } = string.Empty;

        public string CouponCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal DiscountValue { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
