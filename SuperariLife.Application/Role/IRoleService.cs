using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Role;

namespace SuperariLife.Application.Role
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponseModel>> GetAllAsync();
    }
}
