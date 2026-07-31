using System;
using System.Collections.Generic;
using System.Text;
using SuperariLife.Contracts.Role;
using SuperariLife.Infrastructure.DBRepository.Role;

namespace SuperariLife.Application.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(
            IRoleRepository roleRepository
        )
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleResponseModel>>
            GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }
    }
}
