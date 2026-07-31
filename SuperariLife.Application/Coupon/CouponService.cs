using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Coupon;
using SuperariLife.Infrastructure.DBRepository.Coupon;
using SuperariLife.Application.Models;

namespace SuperariLife.Application.Coupon
{
    public class CouponService : ICouponService
    {
        private readonly
        ICouponRepository _couponRepository;

        public CouponService(
            ICouponRepository couponRepository
        )
        {
            _couponRepository = couponRepository;
        }

        public async Task<OperationResult> CreateAsync(
            CreateCouponRequestModel request,
            long createdBy
        )
        {
            if (request.StartDate >= request.ExpiryDate)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message =
                        "Expiry date must be later than start date."
                };
            }

            if (request.DiscountValue <= 0)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message =
                        "Discount value must be greater than zero."
                };
            }

            long couponId =
                await _couponRepository.CreateAsync(
                    request,
                    createdBy
                );

            return new OperationResult
            {
                IsSuccess = couponId > 0,
                Message = couponId > 0
                    ? "Coupon created successfully."
                    : "Unable to create coupon."
            };
        }

        public async Task<IEnumerable<CouponResponseModel>>
            GetAllAsync()
        {
            return await _couponRepository
                .GetAllAsync();
        }

        public async Task<CouponResponseModel?> GetByIdAsync(
            long couponId
        )
        {
            return await _couponRepository
                .GetByIdAsync(couponId);
        }

        public async Task<OperationResult> UpdateAsync(
            long couponId,
            UpdateCouponRequestModel request,
            long modifiedBy
        )
        {
            if (request.StartDate >= request.ExpiryDate)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message =
                        "Expiry date must be later than start date."
                };
            }

            if (request.DiscountValue <= 0)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message =
                        "Discount value must be greater than zero."
                };
            }

            return await _couponRepository
                .UpdateAsync(
                    couponId,
                    request,
                    modifiedBy
                );
        }

        public async Task<OperationResult> DeleteAsync(
            long couponId,
            long modifiedBy
        )
        {
            return await _couponRepository
                .DeleteAsync(
                    couponId,
                    modifiedBy
                );
        }
    }
}
