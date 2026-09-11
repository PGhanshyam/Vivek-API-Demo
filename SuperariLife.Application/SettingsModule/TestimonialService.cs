using SuperariLife.Application.Models;
using SuperariLife.Contracts.SettingsModule;
using SuperariLife.Infrastructure.DBRepository.SettingsModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperariLife.Application.SettingsModule
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _repository;
        public TestimonialService(ITestimonialRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> CreateAsync(TestimonialRequestModel request, long createdBy)
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
                    Message = "Testimonial content is required."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Author))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Author is required."
                };
            }

            if (request.DisplayOrder <= 0)
            {
                var existingTestimonials = await _repository.GetAllAsync();
                int maxOrder = existingTestimonials != null && existingTestimonials.Any()
                    ? existingTestimonials.Max(t => t.DisplayOrder)
                    : 0;
                request.DisplayOrder = maxOrder + 1;
            }

            var testimonialId = await _repository.InsertAsync(request, createdBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "Testimonial created successfully.",
                Data = testimonialId
            };
        }
        public async Task<IEnumerable<TestimonialResponseModel>> GetAllAsync()
        {
            var rawList = await _repository.GetAllAsync();
            var testimonials = (rawList ?? new List<TestimonialResponseModel>()).ToList();

            int currentSeq = 1;
            foreach (var item in testimonials)
            {
                if (item.DisplayOrder <= 0)
                {
                    item.DisplayOrder = currentSeq;
                    await _repository.UpdateAsync(item.TestimonialId, new TestimonialRequestModel
                    {
                        Content = item.Content,
                        Author = item.Author,
                        DisplayOrder = item.DisplayOrder
                    }, 1);
                }
                currentSeq = Math.Max(currentSeq, item.DisplayOrder) + 1;
            }

            return testimonials;
        }
        public async Task<TestimonialResponseModel?> GetByIdAsync(long testimonialId)
        {
            if (testimonialId <= 0)
            {
                return null;
            }

            var result = await _repository.GetByIdAsync(testimonialId);
            if (result != null && result.DisplayOrder <= 0)
            {
                result.DisplayOrder = 1;
            }

            return result;
        }
        public async Task<OperationResult> UpdateAsync(long testimonialId, TestimonialRequestModel request, long modifiedBy)
        {
            if (testimonialId <= 0)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Invalid testimonial ID."
                };
            }

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
                    Message = "Testimonial content is required."
                };
            }

            if (string.IsNullOrWhiteSpace(request.Author))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Author is required."
                };
            }

            var existing = await _repository.GetByIdAsync(testimonialId);

            if (existing == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Testimonial not found."
                };
            }

            await _repository.UpdateAsync(testimonialId, request, modifiedBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "Testimonial updated successfully."
            };
        }
        public async Task<OperationResult> DeleteAsync(long testimonialId, long modifiedBy)
        {
            if (testimonialId <= 0)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Invalid testimonial ID."
                };
            }

            var existing = await _repository.GetByIdAsync(testimonialId);

            if (existing == null)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Testimonial not found."
                };
            }

            await _repository.DeleteAsync(testimonialId, modifiedBy);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "Testimonial deleted successfully."
            };
        }
    }
}

