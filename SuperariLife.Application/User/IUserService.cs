using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.User;
using SuperariLife.Application.Models;
using SuperariLife.Common.Models;

namespace SuperariLife.Application.User
{
    public interface IUserService
    {
        Task<OperationResult> CreateAsync(CreateUserRequestModel request, long createdBy);
        Task<PagedResult<UserResponseModel>> GetAllAsync(int pageNumber, int pageSize, string? searchText, long? roleId, bool? isActive, string sortColumn, string sortDirection);
        Task<UserResponseModel?> GetByIdAsync(long userId);
        Task<OperationResult> UpdateAsync(long userId, UpdateUserRequestModel request, long modifiedBy);
        Task<OperationResult> DeleteAsync(long userId, long modifiedBy);
    }
}
