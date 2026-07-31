using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BCryptHasher = BCrypt.Net.BCrypt;
using SuperariLife.Contracts.User;
using SuperariLife.Application.Models;
using SuperariLife.Infrastructure.DBRepository.User;

namespace SuperariLife.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> CreateAsync(
            CreateUserRequestModel request,
            long createdBy
        )
        {
            string passwordHash =
                BCryptHasher.HashPassword(request.PasswordHash);

            long userId =
                await _userRepository.CreateAsync(
                    request,
                    passwordHash,
                    createdBy
                );

            return new OperationResult
            {
                IsSuccess = userId > 0,
                Message = userId > 0
                    ? "User created successfully."
                    : "Unable to create user."
            };
        }

        public async Task<IEnumerable<UserResponseModel>>
            GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserResponseModel?> GetByIdAsync(
            long userId
        )
        {
            return await _userRepository.GetByIdAsync(
                userId
            );
        }

        public async Task<OperationResult> UpdateAsync(
            long userId,
            UpdateUserRequestModel request,
            long modifiedBy
        )
        {
            return await _userRepository.UpdateAsync(
                userId,
                request,
                modifiedBy
            );
        }

        public async Task<OperationResult> DeleteAsync(
            long userId,
            long modifiedBy
        )
        {
            return await _userRepository.DeleteAsync(
                userId,
                modifiedBy
            );
        }
    }
}
