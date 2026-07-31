using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Application.Models;
using SuperariLife.Contracts.Coupon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.Coupon
{
    public class CouponRepository: BaseRepository, ICouponRepository
    {
        public CouponRepository(
        IConfiguration configuration
    ) : base(configuration)
        {
        }

        public async Task<long> CreateAsync(
            CreateCouponRequestModel request,
            long? createdBy
        )
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<long>(
                "dbo.SP_Coupon_Insert",
                new
                {
                    request.CouponTypeId,
                    request.CouponCode,
                    request.Description,
                    request.StartDate,
                    request.ExpiryDate,
                    request.DiscountValue,
                    request.IsActive,
                    CreatedBy = createdBy
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<CouponResponseModel>>
            GetAllAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<CouponResponseModel>(
                "dbo.SP_Coupon_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<CouponResponseModel?> GetByIdAsync(
            long couponId
        )
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<
                CouponResponseModel
            >(
                "dbo.SP_Coupon_GetById",
                new
                {
                    CouponId = couponId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<OperationResult> UpdateAsync(
     long couponId,
     UpdateCouponRequestModel request,
     long modifiedBy
 )
        {
            using IDbConnection connection = CreateConnection();

            return await connection
                .QuerySingleAsync<OperationResult>(
                    "dbo.SP_Coupon_Update",
                    new
                    {
                        CouponId = couponId,
                        request.CouponTypeId,
                        request.CouponCode,
                        request.Description,
                        request.StartDate,
                        request.ExpiryDate,
                        request.DiscountValue,
                        request.IsActive,
                        ModifiedBy = modifiedBy
                    },
                    commandType:
                        CommandType.StoredProcedure
                );
        }

        public async Task<OperationResult> DeleteAsync(
        long couponId,
        long modifiedBy
         )
        {
            using IDbConnection connection = CreateConnection();

            return await connection
                .QuerySingleAsync<OperationResult>(
                    "dbo.SP_Coupon_Delete",
                    new
                    {
                        CouponId = couponId,
                        ModifiedBy = modifiedBy
                    },
                    commandType:
                        CommandType.StoredProcedure
                );
        }
    }
}
