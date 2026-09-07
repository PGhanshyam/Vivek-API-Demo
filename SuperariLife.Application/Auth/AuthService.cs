using SuperariLife.Contracts.Authentication;
using SuperariLife.Infrastructure.DBRepository.Auth;
using SuperariLife.Application.Models;
using BCryptHasher = BCrypt.Net.BCrypt;
using SuperariLife.Application.JWTServices;
using SuperariLife.Application.EmailServices;


namespace SuperariLife.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        public AuthService(IAuthRepository authRepository, IJwtService jwtService, IEmailService emailService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }
        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponseModel
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResponseModel
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            string token = _jwtService.GenerateToken(user);

            return new LoginResponseModel
            {
                IsSuccess = true,
                Message = "Login successful.",
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName,
                Token = token,
                TokenExpiry = DateTime.UtcNow.AddMinutes(60),
                MustChangePassword = user.MustChangePassword
            };
        }
        public async Task<OperationResult> CreateResetTokenAsync(ForgotPasswordRequestModel request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            // Do not reveal whether the email exists.
            if (user is null)
            {
                return new OperationResult
                {
                    IsSuccess = true,
                    Message = "If the email is registered, a password reset link will be sent.",
                    Data = null!
                };
            }

            string resetToken = Guid.NewGuid().ToString("N");
            DateTime expiry = DateTime.UtcNow.AddMinutes(30);

            await _authRepository.CreateResetTokenAsync(request.Email, resetToken); //expiry

            // Step 6:
            // Create Angular Reset Password URL

            string resetLink = $"http://localhost:4200/reset-password" + $"?token={Uri.EscapeDataString(resetToken)}";

            // Step 7:
            // Create the email HTML

            //string emailBody = $@"
            //<html>
            //<body>

            //      <h2>
            //         Reset Your Password
            //      </h2>

            //      <p>
            //         We received a request to reset
            //         your Superari Life password.
            //      </p>

            //      <p>
            //         Click the button below:
            //      </p>

            //      <a
            //         href=""{resetLink}""
            //         style=""
            //               display:inline-block;
            //               padding:12px 20px;
            //               text-decoration:none;
            //               border-radius:5px;"">
            //               Reset Password
            //     </a>

            //     <p>
            //        This link will expire in
            //        30 minutes.
            //     </p>

            // </body> 
            // </html>
            // ";

string emailBody = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Reset Your Password</title>
    </head>

    <body style="margin: 0; padding: 0; background-color: #F7F9FC; font-family: Arial, Helvetica, sans-serif; color: #111111;">

        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #F7F9FC; padding: 40px 15px;">

            <tr>
                <td align="center">

                    <!-- Main Container -->
                    <table width="600" cellpadding="0" cellspacing="0" border="0" style="max-width: 600px; width: 100%; background-color: #FFFFFF; border: 1px solid #EEF0F3; border-radius: 12px; overflow: hidden;">

                        <!-- Header -->
                        <tr>
                            <td align="center" style="background-color: #0D0D0D; padding: 35px 20px;">

                                <h1 style="margin: 0; color: #FFFFFF; font-size: 28px; font-weight: 600; letter-spacing: 0.5px;">
                                    Superari Life
                                </h1>

                                <p style="margin: 8px 0 0; color: #777777; font-size: 14px;">
                                    Account Security
                                </p>

                            </td>
                        </tr>

                        <!-- Pink Accent -->
                        <tr>
                            <td style="height: 4px; background-color: #C82468; font-size: 0; line-height: 0;">
                                &nbsp;
                            </td>
                        </tr>

                        <!-- Content -->
                        <tr>
                            <td style="padding: 40px 35px;">

                                <h2 style="margin: 0 0 20px; color: #111111; font-size: 24px; font-weight: 600;">
                                    Reset Your Password
                                </h2>

                                <p style="margin: 0 0 18px; font-size: 16px; line-height: 1.6; color: #111111;">
                                    We received a request to reset your
                                    Superari Life password.
                                </p>

                                <p style="margin: 0 0 30px; font-size: 16px; line-height: 1.6; color: #777777;">
                                    Click the button below to create a new password
                                    for your account.
                                </p>

                                <!-- Reset Button -->
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">

                                    <tr>
                                        <td align="center" style="background-color: #C82468; border-radius: 6px;">

                                            <a href="{resetLink}" style="display: inline-block; padding: 14px 30px; font-size: 16px; font-weight: 600; color: #FFFFFF; text-decoration: none; border-radius: 6px;">
                                                Reset Password
                                            </a>

                                        </td>
                                    </tr>

                                </table>

                                <!-- Expiration Notice -->
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 30px; background-color: #F7F9FC; border: 1px solid #EEF0F3; border-left: 4px solid #C82468;">

                                    <tr>
                                        <td style="padding: 16px 18px;">

                                            <p style="margin: 0; font-size: 14px; line-height: 1.5; color: #111111;">
                                                <strong>Important:</strong>
                                                This password reset link will expire
                                                in 30 minutes.
                                            </p>

                                        </td>
                                    </tr>

                                </table>

                                <!-- Security Information -->
                                <p style="margin: 30px 0 0; font-size: 14px; line-height: 1.6; color: #777777;">
                                    If you did not request a password reset,
                                    you can safely ignore this email.
                                    Your password will remain unchanged.
                                </p>

                                <p style="margin: 30px 0 0; font-size: 15px; line-height: 1.6; color: #111111;">
                                    Regards,<br>
                                    <strong>The Superari Life Team</strong>
                                </p>

                            </td>
                        </tr>

                        <!-- Divider -->
                        <tr>
                            <td style="height: 1px; background-color: #EEF0F3; font-size: 0; line-height: 0;">
                                &nbsp;
                            </td>
                        </tr>

                        <!-- Footer -->
                        <tr>
                            <td align="center" style="background-color: #0D0D0D; padding: 22px;">

                                <p style="margin: 0; font-size: 12px; color: #777777;">
                                    © 2026 Superari Life. All rights reserved.
                                </p>

                            </td>
                        </tr>

                    </table>

                </td>
            </tr>

        </table>

    </body>
    </html>
    """;

            // Step 8:
            // Send the email

            await _emailService.SendEmailAsync(user.Email, "Reset Your Superari Life Password", emailBody);

            // Email sending will be added later.
            return new OperationResult
            {
                IsSuccess = true,
                //Message = "Password reset request created successfully.",
                Message = "Password reset link has been sent to your email.",
                //Data = resetLink
                Data = null!
            };
        }
        public async Task<OperationResult> ResetPasswordAsync(ResetPasswordRequestModel request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Passwords do not match."
                };
            }

            string passwordHash = BCryptHasher.HashPassword(request.NewPassword);

            bool isUpdated = await _authRepository.ResetPasswordAsync(request.ResetToken, passwordHash);

            return new OperationResult
            {
                IsSuccess = isUpdated,
                Message = isUpdated
                ? "Password reset successfully."
                : "The reset token is invalid or expired."
            };
        }
        public async Task<OperationResult> ChangePasswordAsync(long userId, ChangePasswordRequestModel request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "New password and confirm password do not match."
                };
            }

            var user = await _authRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            bool isCurrentPasswordValid = BCryptHasher.Verify(request.CurrentPassword, user.PasswordHash);

            if (!isCurrentPasswordValid)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Current password is incorrect."
                };
            }

            string newPasswordHash = BCryptHasher.HashPassword(request.NewPassword);

            return await _authRepository.ChangePasswordAsync(userId, newPasswordHash);
        }
    }
}
