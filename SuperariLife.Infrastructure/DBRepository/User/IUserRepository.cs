using SuperariLife.Application.Models;
using SuperariLife.Contracts.User;
using System;
using System.Collections.Generic;
using System.Text;


namespace SuperariLife.Infrastructure.DBRepository.User
{
    public interface IUserRepository
    {
        Task<long> CreateAsync(
          CreateUserRequestModel request,
          string passwordHash,
          long? createdBy
        );

        Task<IEnumerable<UserResponseModel>> GetAllAsync();

        Task<UserResponseModel?> GetByIdAsync(long userId);

        Task<OperationResult> UpdateAsync(
        long userId,
        UpdateUserRequestModel request,
        long modifiedBy
        );

        Task<OperationResult> DeleteAsync(
            long userId,
            long modifiedBy
        );
    }
}
