using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SuperariLife.Contracts.Role;

namespace SuperariLife.Infrastructure.DBRepository.Role
{
    public class RoleRepository :BaseRepository, IRoleRepository
    {
        public RoleRepository(IConfiguration configuration)
        : base(configuration)
        {
        }

        public async Task<IEnumerable<RoleResponseModel>> GetAllAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<RoleResponseModel>(
                "dbo.SP_Role_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
