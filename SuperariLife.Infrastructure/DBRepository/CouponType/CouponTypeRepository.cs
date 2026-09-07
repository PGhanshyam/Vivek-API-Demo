using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SuperariLife.Contracts.CouponType;

namespace SuperariLife.Infrastructure.DBRepository.CouponType
{
    public class CouponTypeRepository :BaseRepository, ICouponTypeRepository
    {
        public CouponTypeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<CouponTypeResponseModel>>GetAllAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<CouponTypeResponseModel>(
                "SP_CouponType_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
