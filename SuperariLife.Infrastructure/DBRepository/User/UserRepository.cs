using Dapper;
using Microsoft.Extensions.Configuration;
using SuperariLife.Application.Models;
using SuperariLife.Contracts.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SuperariLife.Infrastructure.DBRepository.User
{
    public class UserRepository: BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        : base(configuration)
        {
        }

        public async Task<long> CreateAsync(
            CreateUserRequestModel request,
            string passwordHash,
            long? createdBy
        )
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@RoleId", request.RoleId);
            parameters.Add("@FirstName", request.FirstName);
            parameters.Add("@LastName", request.LastName);
            parameters.Add("@Email", request.Email);

            // PasswordHash will be passed from UserService.
            parameters.Add("@PasswordHash", passwordHash);

            parameters.Add("@PhoneNo", request.PhoneNo);
            parameters.Add("@ProfileImage", request.ProfileImage);
            parameters.Add("@Address", request.Address);
            parameters.Add("@Country", request.Country);
            parameters.Add("@State", request.State);
            parameters.Add("@City", request.City);
            parameters.Add("@ZipCode", request.ZipCode);
            parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@CreatedBy", createdBy);

            return await connection.QuerySingleAsync<long>(
                "dbo.sp_User_Insert",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllAsync()
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<UserResponseModel>(
                "dbo.sp_User_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<UserResponseModel?> GetByIdAsync(
            long userId
        )
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<
                UserResponseModel   
            >(
                "dbo.SP_User_GetById",
                new
                {
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<OperationResult> UpdateAsync(
        long userId,
        UpdateUserRequestModel request,
        long modifiedBy
    )
        {
            using IDbConnection connection = CreateConnection();

            return await connection
                .QuerySingleAsync<OperationResult>(
                    "dbo.sp_User_Update",
                    new
                    {
                        UserId = userId,
                        request.RoleId,
                        request.FirstName,
                        request.LastName,
                        request.Email,
                        request.PhoneNo,
                        request.ProfileImage,
                        request.Address,
                        request.Country,
                        request.State,
                        request.City,
                        request.ZipCode,
                        request.IsActive,
                        ModifiedBy = modifiedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public async Task<OperationResult> DeleteAsync(
            long userId,
            long modifiedBy
        )
        {
            using IDbConnection connection = CreateConnection();

            return await connection
                .QuerySingleAsync<OperationResult>(
                    "dbo.sp_User_Delete",
                    new
                    {
                        UserId = userId,
                        ModifiedBy = modifiedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
    }
   }