using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public interface ISettingContentService
    {
        Task<SettingContentResponseModel?> GetPrivacyPolicyAsync();
        Task<OperationResult> UpdatePrivacyPolicyAsync(SettingContentRequestModel request, long modifiedBy);
        Task<SettingContentResponseModel?> GetTermsConditionsAsync();
        Task<OperationResult> UpdateTermsConditionsAsync(SettingContentRequestModel request, long modifiedBy);
    }
}
