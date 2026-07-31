using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.User;
using SuperariLife.Application.Models;

namespace SuperariLife.Application.User
{
    public interface IUserService
    {
        Task<OperationResult> CreateAsync(
        CreateUserRequestModel request,
        long createdBy
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
