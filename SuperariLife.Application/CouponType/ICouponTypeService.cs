using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.CouponType;

namespace SuperariLife.Application.CouponType
{
    public interface ICouponTypeService
    {
        Task<IEnumerable<CouponTypeResponseModel>> GetAllAsync();
    }
}
