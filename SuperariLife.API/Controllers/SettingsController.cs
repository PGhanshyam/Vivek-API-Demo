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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingContentService _settingContentService;
        public SettingsController(ISettingContentService settingContentService)
        {
            _settingContentService = settingContentService;
        }

        [HttpGet("privacy-policy")]
        public async Task<IActionResult> GetPrivacyPolicy()
        {
            var result = await _settingContentService.GetPrivacyPolicyAsync();

            return Ok(new
            {
                IsSuccess = true,
                Message = "Privacy Policy retrieved successfully.",
                Data = result ?? new SettingContentResponseModel { SettingType = "PrivacyPolicy", Content = "" }
            });
        }

        [HttpPut("privacy-policy")]
        public async Task<IActionResult> UpdatePrivacyPolicy([FromBody] SettingContentRequestModel request)
        {
            long modifiedBy = 1; // Temporary value

            var result = await _settingContentService.UpdatePrivacyPolicyAsync(request, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
  
        [HttpGet("terms-conditions")]
        public async Task<IActionResult> GetTermsConditions()
        {
            var result = await _settingContentService.GetTermsConditionsAsync();

            return Ok(new
            {
                IsSuccess = true,
                Message = "Terms & Conditions retrieved successfully.",
                Data = result ?? new SettingContentResponseModel { SettingType = "TermsConditions", Content = "" }
            });
        }

        [HttpPut("terms-conditions")]
        public async Task<IActionResult> UpdateTermsConditions([FromBody] SettingContentRequestModel request)
        {
            long modifiedBy = 1; // Temporary value

            var result = await _settingContentService.UpdateTermsConditionsAsync(request, modifiedBy);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
