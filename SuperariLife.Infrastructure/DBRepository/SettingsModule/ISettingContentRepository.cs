using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public interface ISettingContentRepository
    {
        Task<SettingContentResponseModel?> GetPrivacyPolicyAsync();
        Task<bool> UpdatePrivacyPolicyAsync(string content, long modifiedBy);
        Task<SettingContentResponseModel?> GetTermsConditionsAsync();
        Task<bool> UpdateTermsConditionsAsync(string content, long modifiedBy);
    }
}
