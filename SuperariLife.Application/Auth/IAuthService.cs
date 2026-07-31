using System;
using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Authentication;
using SuperariLife.Application.Models;

namespace SuperariLife.Application.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseModel> LoginAsync(
             LoginRequestModel request
        );

        Task<OperationResult> ForgotPasswordAsync(
        ForgotPasswordRequestModel request
        );

        Task<OperationResult> ResetPasswordAsync(
            ResetPasswordRequestModel request
        );
    }
}
