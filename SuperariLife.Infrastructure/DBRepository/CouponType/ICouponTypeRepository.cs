using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.CouponType;


namespace SuperariLife.Infrastructure.DBRepository.CouponType
{
    public interface ICouponTypeRepository
    {
        Task<IEnumerable<CouponTypeResponseModel>> GetAllAsync();
    }
}
