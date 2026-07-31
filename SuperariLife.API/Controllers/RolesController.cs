using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperariLife.Application.Role;
using SuperariLife.Common.Helpers;
using SuperariLife.Contracts.Role;

namespace SuperariLife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(
            IRoleService roleService
        )
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles =
                await _roleService.GetAllAsync();

            return Ok(
                new ApiResponse<IEnumerable<RoleResponseModel>>
                {
                    IsSuccess = true,
                    Message =
                        "Roles retrieved successfully.",
                    Data = roles
                }
            );
        }
    }
}
