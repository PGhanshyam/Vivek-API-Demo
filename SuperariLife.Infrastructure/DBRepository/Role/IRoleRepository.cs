using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Role;

namespace SuperariLife.Infrastructure.DBRepository.Role
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleResponseModel>> GetAllAsync();
    }
}
