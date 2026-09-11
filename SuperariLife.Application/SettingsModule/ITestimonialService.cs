using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public interface ITestimonialService
    {
        Task<OperationResult> CreateAsync(TestimonialRequestModel request, long createdBy);
        Task<IEnumerable<TestimonialResponseModel>> GetAllAsync();
        Task<TestimonialResponseModel?> GetByIdAsync(long testimonialId);
        Task<OperationResult> UpdateAsync(long testimonialId, TestimonialRequestModel request, long modifiedBy);
        Task<OperationResult> DeleteAsync(long testimonialId, long modifiedBy);
    }
}
