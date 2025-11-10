using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vms.application.Interfaces.Services;
using vms.domain.Entities;
using vms.shared.DTO.Roles;

namespace vms.presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto dto)
        {
            var createdRole = await _roleService.CreateRoleAsync(dto);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(long id, [FromBody] RoleUpdateDto dto)
        {
            var updatedRole = await _roleService.UpdateRoleAsync(id, dto);
            if (updatedRole == null) return NotFound();
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var success = await _roleService.DeleteRoleAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
