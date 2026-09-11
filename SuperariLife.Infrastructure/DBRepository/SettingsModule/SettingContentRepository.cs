using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public class SettingContentRepository : BaseRepository, ISettingContentRepository
    {
        public SettingContentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<SettingContentResponseModel?> GetPrivacyPolicyAsync()
        {
            using IDbConnection connection = CreateConnection();

            var result = await connection.QuerySingleOrDefaultAsync<SettingContentResponseModel>(
                "SP_SettingContent_GetPrivacyPolicy",
                commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<bool> UpdatePrivacyPolicyAsync(string content, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@Content", content);
            parameters.Add("@ModifiedBy", modifiedBy);

            await connection.ExecuteAsync(
                "SP_SettingContent_UpdatePrivacyPolicy",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }
        public async Task<SettingContentResponseModel?> GetTermsConditionsAsync()
        {
            using IDbConnection connection = CreateConnection();

            var result = await connection.QuerySingleOrDefaultAsync<SettingContentResponseModel>(
                "SP_SettingContent_GetTermsConditions",
                commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<bool> UpdateTermsConditionsAsync(string content, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@Content", content);
            parameters.Add("@ModifiedBy", modifiedBy);

            await connection.ExecuteAsync(
                "SP_SettingContent_UpdateTermsConditions",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
