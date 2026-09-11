using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public interface IGeneralSettingsService
    {
        Task<GeneralSettingsResponseModel?> GetAsync();
        Task<OperationResult> UpdateAsync(GeneralSettingsRequestModel request, long modifiedBy);
    }
}
