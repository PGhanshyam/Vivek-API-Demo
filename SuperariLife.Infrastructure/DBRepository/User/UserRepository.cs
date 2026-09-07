using Dapper;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using SuperariLife.Application.Models;
using SuperariLife.Common.Models;
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

        public async Task<long> CreateAsync(CreateUserRequestModel request, string passwordHash, bool mustChangePassword, long? createdBy)
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
            parameters.Add("@ProfileImage", request.ProfileImagePath);
            parameters.Add("@Address", request.Address);
            parameters.Add("@Country", request.Country);
            parameters.Add("@State", request.State);
            parameters.Add("@City", request.City);
            parameters.Add("@ZipCode", request.ZipCode);
            //parameters.Add("@IsActive", request.IsActive);
            parameters.Add("@CreatedBy", createdBy);

            // New users must change the temporary password
            parameters.Add("@MustChangePassword", mustChangePassword);

            return await connection.QuerySingleAsync<long>(
                "SP_User_Insert",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<PagedResult<UserResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, long? roleId, bool? isActive, string sortColumn, string sortDirection)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@SearchText", searchText);
            parameters.Add("@RoleId", roleId);
            parameters.Add("@IsActive", isActive);
            parameters.Add("@SortColumn", sortColumn);
            parameters.Add("@SortDirection", sortDirection);

            using var multi = await connection.QueryMultipleAsync(
                "SP_User_GetAll",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var users = await multi.ReadAsync<UserResponseModel>();
            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            return new PagedResult<UserResponseModel>
            { 
                Items = users,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<UserResponseModel?> GetByIdAsync(long userId)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<UserResponseModel>(
                "SP_User_GetById",
                new
                {
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<OperationResult> UpdateAsync(long userId, UpdateUserRequestModel request, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                    "SP_User_Update",
                    new
                    {
                        UserId = userId,
                        request.RoleId,
                        request.FirstName,
                        request.LastName,
                        request.Email,
                        request.PhoneNo,
                        ProfileImage = request.ProfileImagePath,
                        request.Address,
                        request.Country,
                        request.State,
                        request.City,
                        request.ZipCode,
                        IsActive = request.IsActive,
                        ModifiedBy = modifiedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
        public async Task<OperationResult> DeleteAsync(long userId, long modifiedBy)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                    "SP_User_Delete",
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