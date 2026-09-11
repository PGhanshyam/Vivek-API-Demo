using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.SettingsModule;
using SuperariLife.Contracts.SettingsModule;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        public TestimonialsController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _testimonialService.GetAllAsync();

            return Ok(new
            {
                IsSuccess = true,
                Message = "Testimonials retrieved successfully.",
                Data = result
            });
        }
 
        [HttpGet("{testimonialId:long}")]
        public async Task<IActionResult> GetById(long testimonialId)
        {
            var result = await _testimonialService.GetByIdAsync(testimonialId);

            if (result == null)
            {
                return NotFound(new
                {
                    IsSuccess = false,
                    Message = "Testimonial not found."
                });
            }

            return Ok(new
            {
                IsSuccess = true,
                Message = "Testimonial retrieved successfully.",
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestimonialRequestModel request)
        {
            long createdBy = 1; // Temporary value

            var result = await _testimonialService.CreateAsync(request, createdBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{testimonialId:long}")]
        public async Task<IActionResult> Update(long testimonialId, [FromBody] TestimonialRequestModel request)
        {
            long modifiedBy = 1; // Temporary value

            var result = await _testimonialService.UpdateAsync(testimonialId, request, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{testimonialId:long}")]
        public async Task<IActionResult> Delete(long testimonialId)
        {
            long modifiedBy = 1; // Temporary value

            var result = await _testimonialService.DeleteAsync(testimonialId, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
