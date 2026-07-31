using SuperariLife.Contracts.Authentication;
using SuperariLife.Infrastructure.DBRepository.Auth;
using SuperariLife.Application.Models;
using BCryptHasher = BCrypt.Net.BCrypt;
using SuperariLife.Application.JWTServices;


namespace SuperariLife.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly
        IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IAuthRepository authRepository,
            IJwtService jwtService
        )
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseModel>
    LoginAsync(
        LoginRequestModel request)
        {
            var user =
                await _authRepository
                    .GetUserByEmailAsync(
                        request.Email
                    );

            if (user == null)
            {
                return new LoginResponseModel
                {
                    IsSuccess = false,
                    Message =
                        "Invalid email or password."
                };
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash
                );

            if (!isPasswordValid)
            {
                return new LoginResponseModel
                {
                    IsSuccess = false,
                    Message =
                        "Invalid email or password."
                };
            }

            string token =
                _jwtService.GenerateToken(
                    user
                );

            return new LoginResponseModel
            {
                IsSuccess = true,
                Message =
                    "Login successful.",

                UserId =
                    user.UserId,

                FirstName =
                    user.FirstName,

                LastName =
                    user.LastName,

                Email =
                    user.Email,

                RoleName =
                    user.RoleName,

                Token =
                    token,

                TokenExpiry =
                    DateTime.UtcNow.AddMinutes(
                        60
                    )
            };
        }

        public async Task<OperationResult>
            ForgotPasswordAsync(
                ForgotPasswordRequestModel request
            )
        {
            var user =
                await _authRepository
                    .GetUserByEmailAsync(
                        request.Email
                    );

            // Do not reveal whether the email exists.
            if (user is null)
            {
                return new OperationResult
                {
                    IsSuccess = true,
                    Message =
                        "If the email is registered, a password reset link will be sent."
                };
            }

            string resetToken =
                Guid.NewGuid().ToString("N");

            DateTime expiry =
                DateTime.UtcNow.AddMinutes(30);

            await _authRepository
                .CreateResetTokenAsync(
                    request.Email,
                    resetToken,
                    expiry
                );

            // Email sending will be added later.
            return new OperationResult
            {
                IsSuccess = true,
                Message =
                    "Password reset request created successfully."
            };
        }

        public async Task<OperationResult>
            ResetPasswordAsync(
                ResetPasswordRequestModel request
            )
        {
            if (
                request.NewPassword
                != request.ConfirmPassword
            )
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message =
                        "Passwords do not match."
                };
            }

            string passwordHash =
                BCryptHasher.HashPassword(
                    request.NewPassword
                );

            bool isUpdated =
                await _authRepository
                    .ResetPasswordAsync(
                        request.ResetToken,
                        passwordHash
                    );

            return new OperationResult
            {
                IsSuccess = isUpdated,
                Message = isUpdated
                    ? "Password reset successfully."
                    : "The reset token is invalid or expired."
            };
        }
    }
}
