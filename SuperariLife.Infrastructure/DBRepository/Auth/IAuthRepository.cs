using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Application.Models;
using SuperariLife.Contracts.Authentication;

namespace SuperariLife.Infrastructure.DBRepository.Auth
{
    public interface IAuthRepository
    {
        Task<LoginUserResponseModel?> GetUserByEmailAsync(string email);

        Task CreateResetTokenAsync(
            string email,
            string resetPasswordToken,
            DateTime expiry
        );

        //Task<OperationResult>
        //ForgotPasswordAsync(
        //    string email,
        //    string resetPasswordToken
        //);

        Task<OperationResult>
            ResetPasswordAsync(
                string resetPasswordToken,
                string passwordHash
            );
    }
}
