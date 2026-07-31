using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.CouponType;
using SuperariLife.Infrastructure.DBRepository.CouponType;

namespace SuperariLife.Application.CouponType
{
    public class CouponTypeService : ICouponTypeService
    {
        private readonly
        ICouponTypeRepository _couponTypeRepository;

        public CouponTypeService(
            ICouponTypeRepository couponTypeRepository
        )
        {
            _couponTypeRepository =
                couponTypeRepository;
        }

        public async Task<
            IEnumerable<CouponTypeResponseModel>
        > GetAllAsync()
        {
            return await _couponTypeRepository
                .GetAllAsync();
        }
    }
}
