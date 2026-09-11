using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using SuperariLife.Infrastructure.DBRepository.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public class SettingContentService : ISettingContentService
    {
        private readonly ISettingContentRepository _repository;
        public SettingContentService(ISettingContentRepository repository)
        {
            _repository = repository;
        }
        public async Task<SettingContentResponseModel?> GetPrivacyPolicyAsync()
        {
            return await _repository.GetPrivacyPolicyAsync();
        }
        public async Task<OperationResult> UpdatePrivacyPolicyAsync(SettingContentRequestModel request, long modifiedBy)
        {
            if (request == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Request cannot be null."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Privacy Policy content is required."
                };
            }

            await _repository.UpdatePrivacyPolicyAsync(request.Content, modifiedBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "Privacy Policy updated successfully."
            };
        }
        public async Task<SettingContentResponseModel?> GetTermsConditionsAsync()
        {
            return await _repository.GetTermsConditionsAsync();
        }
        public async Task<OperationResult> UpdateTermsConditionsAsync(SettingContentRequestModel request, long modifiedBy)
        {
            if (request == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Request cannot be null."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Terms & Conditions content is required."
                };
            }

            await _repository.UpdateTermsConditionsAsync(request.Content, modifiedBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "Terms & Conditions updated successfully."
            };
        }       
    }
}

