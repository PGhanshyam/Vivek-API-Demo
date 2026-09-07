using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Contracts.CouponType
{
    public class CouponTypeResponseModel
    {
        public long CouponTypeId { get; set; }
        public string CouponTypeName { get; set; } = string.Empty;
    }
}
