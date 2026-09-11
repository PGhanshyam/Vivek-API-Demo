using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public class TestimonialRepository : BaseRepository, ITestimonialRepository
    {
        public TestimonialRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<long> InsertAsync(TestimonialRequestModel request, long createdBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@Content", request.Content);
            parameters.Add("@Author", request.Author);
            parameters.Add("@DisplayOrder", request.DisplayOrder);
            parameters.Add("@CreatedBy", createdBy);

            var result = await connection.QuerySingleAsync<long>(
                "SP_Testimonial_Insert",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<IEnumerable<TestimonialResponseModel>> GetAllAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<TestimonialResponseModel>(
                "SP_Testimonial_GetAll",
                commandType: CommandType.StoredProcedure);
        }
        public async Task<TestimonialResponseModel?> GetByIdAsync(long testimonialId)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@TestimonialId", testimonialId);

            return await connection.QuerySingleOrDefaultAsync<TestimonialResponseModel>(
                "SP_Testimonial_GetById",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> UpdateAsync(long testimonialId, TestimonialRequestModel request, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@TestimonialId", testimonialId);
            parameters.Add("@Content", request.Content);
            parameters.Add("@Author", request.Author);
            parameters.Add("@DisplayOrder", request.DisplayOrder);
            parameters.Add("@ModifiedBy", modifiedBy);

            await connection.ExecuteAsync(
                "SP_Testimonial_Update",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }
        public async Task<bool> DeleteAsync(long testimonialId, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@TestimonialId", testimonialId);
            parameters.Add("@ModifiedBy", modifiedBy);

            await connection.ExecuteAsync(
                "SP_Testimonial_Delete",
                parameters,
                commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}

