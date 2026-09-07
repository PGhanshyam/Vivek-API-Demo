using SuperariLife.Application.Models;
using SuperariLife.Common.Models;
using SuperariLife.Contracts.Coupon;
using System;
using System.Collections.Generic;
using System.Text;


namespace SuperariLife.Infrastructure.DBRepository.Coupon
{
    public interface ICouponRepository
    {
        Task<long> CreateAsync(CreateCouponRequestModel request, long? createdBy);
        Task<PagedResult<CouponResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, int? couponTypeId, bool? isActive, string sortColumn, string sortDirection);
        Task<CouponResponseModel?> GetByIdAsync(long couponId);
        Task<OperationResult> UpdateAsync(long couponId, UpdateCouponRequestModel request, long modifiedBy);
        Task<OperationResult> DeleteAsync(long couponId, long modifiedBy);
    }
}
