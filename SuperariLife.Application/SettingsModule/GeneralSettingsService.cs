using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using SuperariLife.Infrastructure.DBRepository.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IGeneralSettingsRepository _repository;
        public GeneralSettingsService(IGeneralSettingsRepository repository)
        {
            _repository = repository;
        }
        public async Task<GeneralSettingsResponseModel?> GetAsync()
        {
            return await _repository.GetAsync();
        }
        public async Task<OperationResult> UpdateAsync(GeneralSettingsRequestModel request, long modifiedBy)
        {
            if (request == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Request cannot be null."
                };
            }

            if (string.IsNullOrWhiteSpace(request.AdminEmail))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Admin email is required."
                };
            }

            if (request.TaxPercentage < 0 || request.TaxPercentage > 100)

            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Tax percentage must be between 0 and 100."
                };
            }

            await _repository.UpdateAsync(request, modifiedBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "General settings updated successfully."
            };
        }
    }
}

