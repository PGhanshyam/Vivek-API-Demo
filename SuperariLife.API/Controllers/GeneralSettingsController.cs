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
    public class GeneralSettingsController : ControllerBase
    {
        private readonly IGeneralSettingsService _generalSettingsService;
        public GeneralSettingsController(IGeneralSettingsService generalSettingsService)
        {
            _generalSettingsService = generalSettingsService;
        }
     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _generalSettingsService.GetAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    IsSuccess = false,
                    Message = "General settings not found."
                });
            }

            return Ok(new
            {
                IsSuccess = true,
                Message = "General settings retrieved successfully.",
                Data = result
            });
        }
       
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GeneralSettingsRequestModel request)
        {
            long modifiedBy = 1; // Temporary value

            var result = await _generalSettingsService.UpdateAsync(request, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
