using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.Auth;
using SuperariLife.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using SuperariLife.Common.Helpers;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestModel request
        )
        {
            var result =
                await _authService.LoginAsync(request);

            if (!result.IsSuccess)
            {
                return Unauthorized(
                    new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    }
                );
            }

            return Ok(
                new ApiResponse<object>
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = null
                }
            );
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgotPasswordRequestModel request
        )
        {
            var result =
                await _authService
                    .ForgotPasswordAsync(request);

            return Ok(
                new ApiResponse<object>
                {
                    IsSuccess = result.IsSuccess,
                    Message = result.Message,
                    Data = null
                }
            );
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordRequestModel request
        )
        {
            var result =
                await _authService
                    .ResetPasswordAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(
                    new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    }
                );
            }

            return Ok(
                new ApiResponse<object>
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = null
                }
            );
        }
    }
}
