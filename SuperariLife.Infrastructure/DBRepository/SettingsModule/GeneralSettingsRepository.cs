using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public class GeneralSettingsRepository : BaseRepository, IGeneralSettingsRepository
    {
        public GeneralSettingsRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<GeneralSettingsResponseModel?> GetAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<GeneralSettingsResponseModel>(
                "SP_GeneralSettings_Get",
                commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> UpdateAsync(GeneralSettingsRequestModel request, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@AdminEmail", request.AdminEmail);
            parameters.Add("@TaxPercentage", request.TaxPercentage);
            parameters.Add("@FacebookUrl", request.FacebookUrl);
            parameters.Add("@TwitterUrl", request.TwitterUrl);
            parameters.Add("@InstagramUrl", request.InstagramUrl);
            parameters.Add("@YoutubeUrl", request.YoutubeUrl);
            parameters.Add("@ModifiedBy", modifiedBy);

            await connection.ExecuteAsync(
                "SP_GeneralSettings_Update",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}

