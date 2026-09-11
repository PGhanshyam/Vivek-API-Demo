using SuperariLife.Contracts.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Infrastructure.DBRepository.SettingsModule
{
    public interface ITestimonialRepository
    {
        Task<long> InsertAsync(TestimonialRequestModel request, long createdBy);
        Task<IEnumerable<TestimonialResponseModel>> GetAllAsync();
        Task<TestimonialResponseModel?> GetByIdAsync(long testimonialId);
        Task<bool> UpdateAsync(long testimonialId, TestimonialRequestModel request, long modifiedBy);
        Task<bool> DeleteAsync(long testimonialId, long modifiedBy);
    }
}
