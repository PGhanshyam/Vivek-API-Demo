using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Coupon;
using SuperariLife.Application.Models;

namespace SuperariLife.Application.Coupon
{
    public interface ICouponService
    {
        Task<OperationResult> CreateAsync(
        CreateCouponRequestModel request,
        long createdBy
        );

        Task<IEnumerable<CouponResponseModel>> GetAllAsync();

        Task<CouponResponseModel?> GetByIdAsync(
            long couponId
        );

        Task<OperationResult> UpdateAsync(
            long couponId,
            UpdateCouponRequestModel request,
            long modifiedBy
        );

        Task<OperationResult> DeleteAsync(
            long couponId,
            long modifiedBy
        );
    }
}
