using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.User;
using SuperariLife.Common.Helpers;
using SuperariLife.Contracts.User;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService
        )
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserRequestModel request
        )
        {
            // Temporary value.
            // In Phase 11, this will come from JWT.
            long createdBy = 1;

            var result =
                await _userService.CreateAsync(
                    request,
                    createdBy
                );

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =
                await _userService.GetAllAsync();

            return Ok(
                new ApiResponse<IEnumerable<UserResponseModel>>
                {
                    IsSuccess = true,
                    Message =
                        "Users retrieved successfully.",
                    Data = users
                }
            );
        }

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetById(
            long userId
        )
        {
            var user =
                await _userService.GetByIdAsync(
                    userId
                );

            if (user is null)
            {
                return NotFound(
                    new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = "User not found.",
                        Data = null
                    }
                );
            }

            return Ok(
                new ApiResponse<UserResponseModel>
                {
                    IsSuccess = true,
                    Message =
                        "User retrieved successfully.",
                    Data = user
                }
            );
        }

        [HttpPut("{userId:long}")]
        public async Task<IActionResult> Update(
            long userId,
            [FromBody] UpdateUserRequestModel request
        )
        {
            // Temporary value.
            // JWT user ID will be used in Phase 11.
            long modifiedBy = 1;

            var result =
                await _userService.UpdateAsync(
                    userId,
                    request,
                    modifiedBy
                );

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

        [HttpDelete("{userId:long}")]
        public async Task<IActionResult> Delete(
            long userId
        )
        {
            // Temporary value.
            // JWT user ID will be used in Phase 11.
            long modifiedBy = 1;

            var result =
                await _userService.DeleteAsync(
                    userId,
                    modifiedBy
                );

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
