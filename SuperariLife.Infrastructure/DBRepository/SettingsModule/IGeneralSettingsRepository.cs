using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public interface IGeneralSettingsRepository
    {
        Task<GeneralSettingsResponseModel?> GetAsync();
        Task<bool> UpdateAsync(GeneralSettingsRequestModel request, long modifiedBy);
    }
}
