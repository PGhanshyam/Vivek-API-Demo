using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.Coupon;
using SuperariLife.Common.Helpers;
using SuperariLife.Common.Models;
using SuperariLife.Contracts.Coupon;
using System.Security.Claims;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Yoga Instructor")]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCouponRequestModel request)
        {
            // Temporary value.
            // JWT user ID will be used in Phase 11.
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

            var result = await _couponService.CreateAsync(request, createdBy);

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
        public async Task<IActionResult> GetAll(
              [FromQuery] int pageNumber = 1,
              [FromQuery] int pageSize = 10,
              [FromQuery] string? searchText = null,
              [FromQuery] int? couponTypeId = null,
              [FromQuery] bool? isActive = null,
              [FromQuery] string sortColumn = "CouponCode",
              [FromQuery] string sortDirection = "ASC"
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

            var coupons = await _couponService.GetAllAsync(pageNumber, pageSize, searchText, couponTypeId, isActive, sortColumn, sortDirection);

            return Ok(new ApiResponse<PagedResult<CouponResponseModel>>
                {
                    IsSuccess = true,
                    Message = "Coupons retrieved successfully.",
                    Data = coupons
                }
            );
        }

        [HttpGet("{couponId:long}")]
        public async Task<IActionResult> GetById(long couponId)
        {
            var coupon = await _couponService.GetByIdAsync(couponId);

            if (coupon is null)
            {
                return NotFound(new ApiResponse<object>
                    {
                        IsSuccess = false,
                        Message = "Coupon not found.",
                        Data = null
                    }
                );
            }

                return Ok(new ApiResponse<CouponResponseModel>
                    {
                        IsSuccess = true,
                        Message = "Coupon retrieved successfully.",
                        Data = coupon
                    }
                );
        }

        [HttpPut("{couponId:long}")]
        public async Task<IActionResult> Update(long couponId, [FromBody] UpdateCouponRequestModel request)
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

            var result = await _couponService.UpdateAsync(couponId, request, modifiedBy);

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

        [HttpDelete("{couponId:long}")]
        public async Task<IActionResult> Delete(long couponId)
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

            var result = await _couponService.DeleteAsync(couponId, modifiedBy);

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
