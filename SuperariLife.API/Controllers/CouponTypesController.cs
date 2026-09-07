using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.CouponType;
using SuperariLife.Common.Helpers;
using SuperariLife.Contracts.CouponType;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CouponTypesController : ControllerBase
    {
        private readonly ICouponTypeService _couponTypeService;
        public CouponTypesController(ICouponTypeService couponTypeService)
        {
            _couponTypeService = couponTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var couponTypes = await _couponTypeService.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<CouponTypeResponseModel>>
                {
                    IsSuccess = true,
                    Message = "Coupon types retrieved successfully.",
                    Data = couponTypes
                }
            );
        }
    }
}
