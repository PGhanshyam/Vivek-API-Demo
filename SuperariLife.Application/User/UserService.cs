using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BCryptHasher = BCrypt.Net.BCrypt;
using SuperariLife.Contracts.User;
using SuperariLife.Application.Models;
using SuperariLife.Infrastructure.DBRepository.User;
using SuperariLife.Common.Models;
using SuperariLife.Application.PasswordServices;
using SuperariLife.Application.EmailServices;

namespace SuperariLife.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<OperationResult> CreateAsync(CreateUserRequestModel request, long createdBy)
        {
            // Save image here
            if (request.ProfileImage != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ProfileImage.FileName);

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "users");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await request.ProfileImage.CopyToAsync(stream);

                // Save image path in request model
                request.ProfileImagePath = $"uploads/users/{fileName}";
            }

            string temporaryPassword = PasswordGenerator.Generate();

            //string passwordToHash = !string.IsNullOrWhiteSpace(request.PasswordHash)
            //    ? request.PasswordHash
            //    : temporaryPassword;

            string passwordHash = BCryptHasher.HashPassword(temporaryPassword);

            bool mustChangePassword = true;

            long userId = await _userRepository.CreateAsync(request, passwordHash, mustChangePassword, createdBy);

            //return new OperationResult
            //{
            //    IsSuccess = userId > 0,
            //    Message = userId > 0
            //        ? "User created successfully."
            //        : "Unable to create user."
            //};

            if (userId <= 0)
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "Unable to create user."
                };
            }

            
            
                string emailBody = EmailTemplates.WelcomeUser(request.FirstName, request.Email, temporaryPassword);

                await _emailService.SendEmailAsync(request.Email, "Welcome to Superari Life", emailBody);
            
            
            
                return new OperationResult
                {
                    IsSuccess = true,
                    Message = "User created successfully." + "Login credentials have been sent to the registered email address."
                };
            
        }
        public async Task<PagedResult<UserResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, long? roleId, bool? isActive, string sortColumn, string sortDirection)
        {
            return await _userRepository.GetAllAsync(pageNumber, pageSize, searchText, roleId, isActive, sortColumn, sortDirection);
        }
        public async Task<UserResponseModel?> GetByIdAsync(long userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
        public async Task<OperationResult> UpdateAsync(long userId, UpdateUserRequestModel request, long modifiedBy)
        {
            if (request.ProfileImage != null && request.ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ProfileImage.FileName);

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "users");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(stream);
                }

                request.ProfileImagePath = $"uploads/users/{fileName}";
            }

            return await _userRepository.UpdateAsync(userId, request, modifiedBy);
        }
        public async Task<OperationResult> DeleteAsync(long userId, long modifiedBy)
        {
            return await _userRepository.DeleteAsync(userId, modifiedBy);
        }
    }
}
