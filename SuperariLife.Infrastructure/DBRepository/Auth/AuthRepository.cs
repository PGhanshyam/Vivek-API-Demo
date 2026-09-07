using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SuperariLife.Application.Models;
using SuperariLife.Contracts.Authentication;

namespace SuperariLife.Infrastructure.DBRepository.Auth
{
    public class AuthRepository: BaseRepository, IAuthRepository
    {
        public AuthRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<LoginUserResponseModel?> GetUserByEmailAsync(string email)
        {
            using IDbConnection connection = CreateConnection();

            var parameters = new DynamicParameters();

            parameters.Add("@Email", email);

            var user = await connection.QueryFirstOrDefaultAsync<LoginUserResponseModel>(
                        "SP_User_Login",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

            return user;
        }

        public async Task CreateResetTokenAsync(string email, string resetToken) //DateTime resetTokenExpiry
        {
            using IDbConnection connection = CreateConnection();

            await connection.ExecuteAsync(
                "SP_User_CreateResetToken",
                new
                {
                    Email = email,
                    ResetPasswordToken = resetToken,
                    //ResetTokenExpiry = resetTokenExpiry
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<OperationResult> ResetPasswordAsync(string resetToken, string passwordHash)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                    "SP_User_ResetPassword",
                    new
                    {
                        ResetToken = resetToken,
                        PasswordHash = passwordHash
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public async Task<OperationResult> ChangePasswordAsync(long userId, string passwordHash)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QuerySingleAsync<OperationResult>(
                "SP_User_ChangePassword",
                new
                {
                    UserId = userId,
                    PasswordHash = passwordHash
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<LoginUserResponseModel?> GetUserByIdAsync(long userId)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<LoginUserResponseModel>(
                "SP_GetByIdForPasswordChange",
                new
                {
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
