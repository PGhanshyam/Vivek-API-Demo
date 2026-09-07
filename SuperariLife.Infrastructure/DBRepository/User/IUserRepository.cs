using SuperariLife.Application.Models;
using SuperariLife.Common.Models;
using SuperariLife.Contracts.User;
using System;
using System.Collections.Generic;
using System.Text;


namespace SuperariLife.Infrastructure.DBRepository.User
{
    public interface IUserRepository
    {
        Task<long> CreateAsync(CreateUserRequestModel request, string passwordHash, bool mustChangePassword, long? createdBy);
        Task<PagedResult<UserResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, long? roleId, bool? isActive, string sortColumn, string sortDirection);
        Task<UserResponseModel?> GetByIdAsync(long userId);
        Task<OperationResult> UpdateAsync(long userId, UpdateUserRequestModel request, long modifiedBy);
        Task<OperationResult> DeleteAsync(long userId, long modifiedBy);
    }
}
