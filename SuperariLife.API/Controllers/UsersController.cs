using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.User;
using SuperariLife.Common.Helpers;
using SuperariLife.Common.Models;
using SuperariLife.Contracts.User;
using System.Security.Claims;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateUserRequestModel request)
        {
            // Temporary value.

            //long createdBy = 1;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out long createdBy))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Unable to identify the logged-in user.",
                    Data = null
                });
            }

            var result = await _userService.CreateAsync(request, createdBy);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    }
                );
            }

                return Ok(new ApiResponse<object>
                    {
                       IsSuccess = true,
                       Message = result.Message,
                       Data = null
                    }
                );
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Yoga Instructor")]
        public async Task<IActionResult> GetAll(
              [FromQuery] int pageNumber = 1,
              [FromQuery] int pageSize = 10,
              [FromQuery] string? searchText = null,
              [FromQuery] long? roleId = null,
              [FromQuery] bool? isActive = null,
              [FromQuery] string sortColumn = "FirstName",
              [FromQuery] string sortDirection = "DESC"
            )
        {
            if(pageNumber < 1)
            {
                pageNumber = 1;
            }

            if(pageSize < 1)
            {
                pageSize = 10;
            }

            if(pageSize > 100)
            {
                pageSize = 100;
            }

            var users = await _userService.GetAllAsync(pageNumber, pageSize, searchText, roleId, isActive, sortColumn, sortDirection);

            return Ok(new ApiResponse<PagedResult<UserResponseModel>>
                {
                    IsSuccess = true,
                    Message = "Users retrieved successfully.",
                    Data = users
                }
            );
        }

        [HttpGet("{userId:long}")]
        [Authorize(Roles = "Admin, Yoga Instructor")]
        public async Task<IActionResult> GetById(long userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound(new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = "User not found.",
                        Data = null
                    }
                );
            }

            return Ok(new ApiResponse<UserResponseModel>
                {
                    IsSuccess = true,
                    Message = "User retrieved successfully.",
                    Data = user
                }
            );
        }

        [HttpPut("{userId:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long userId, [FromForm] UpdateUserRequestModel request)
        {
            // Temporary value.
            // JWT user ID will be used in Phase 11.
            //long modifiedBy = 1;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out long modifiedBy))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Unable to identify the logged-in user.",
                    Data = null
                });
            }

            var result = await _userService.UpdateAsync(userId, request, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    }
                );
            }

            return Ok(new ApiResponse<object>
                {
                    IsSuccess = true,
                    Message = result.Message,
                    Data = null
                }
            );
        }

        [HttpDelete("{userId:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long userId)
        {
            // Temporary value.
            // JWT user ID will be used in Phase 11.
            //long modifiedBy = 1;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out long modifiedBy))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Unable to identify the logged-in user.",
                    Data = null
                });
            }

            var result = await _userService.DeleteAsync(userId, modifiedBy);

            if (!result.IsSuccess)
            {
                 return BadRequest(new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = null
                    }
                 );
            }

                 return Ok(new ApiResponse<object>
                    {
                        IsSuccess = true,
                        Message = result.Message,
                        Data = null
                    }
                 );
        }
    }
}
