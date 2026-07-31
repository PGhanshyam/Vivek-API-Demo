using SuperariLife.Application.Models;
using SuperariLife.Contracts.Coupon;
using System;
using System.Collections.Generic;
using System.Text;


namespace SuperariLife.Infrastructure.DBRepository.Coupon
{
    public interface ICouponRepository
    {
        Task<long> CreateAsync(
        CreateCouponRequestModel request,
        long? createdBy
    );

        Task<IEnumerable<CouponResponseModel>> GetAllAsync();

        Task<CouponResponseModel?> GetByIdAsync(long couponId);

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
