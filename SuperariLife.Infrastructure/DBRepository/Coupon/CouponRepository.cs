using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Application.Models;
using SuperariLife.Common.Models;
using SuperariLife.Contracts.Coupon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.Coupon
{
    public class CouponRepository: BaseRepository, ICouponRepository
    {
        public CouponRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<long> CreateAsync(CreateCouponRequestModel request, long? createdBy)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<long>(
                "SP_Coupon_Insert",
                new
                {
                    request.CouponTypeId,
                    request.CouponCode,
                    request.Description,
                    request.StartDate,
                    request.ExpiryDate,
                    request.DiscountType,
                    request.DiscountValue,
                    CreatedBy = createdBy
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<PagedResult<CouponResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, int? couponTypeId, bool? isActive, string sortColumn, string sortDirection)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@SearchText", searchText);
            parameters.Add("@CouponTypeId", couponTypeId);
            parameters.Add("@IsActive", isActive);
            parameters.Add("@SortColumn", sortColumn);
            parameters.Add("@SortDirection", sortDirection);

            using var multi = await connection.QueryMultipleAsync(
                "SP_Coupon_GetAll",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var coupons = await multi.ReadAsync<CouponResponseModel>();
            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            return new PagedResult<CouponResponseModel>
            {
                Items = coupons,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<CouponResponseModel?> GetByIdAsync(long couponId)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<CouponResponseModel>(
                "SP_Coupon_GetById",
                new
                {
                    CouponId = couponId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<OperationResult> UpdateAsync(long couponId, UpdateCouponRequestModel request, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                    "SP_Coupon_Update",
                    new
                    {
                        CouponId = couponId,
                        request.CouponTypeId,
                        request.CouponCode,
                        request.Description,
                        request.StartDate,
                        request.ExpiryDate,
                        request.DiscountType,
                        request.DiscountValue,
                        request.IsActive,
                        ModifiedBy = modifiedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public async Task<OperationResult> DeleteAsync(long couponId, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                    "SP_Coupon_Delete",
                    new
                    {
                        CouponId = couponId,
                        ModifiedBy = modifiedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
    }
}
