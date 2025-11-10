using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.domain.Entities;
using vms.shared.DTO.Roles;

namespace vms.application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();
        Task<RoleResponseDto> GetRoleByIdAsync(long id);
        Task<RoleResponseDto> CreateRoleAsync(RoleCreateDto dto);
        Task<RoleResponseDto> UpdateRoleAsync(long id, RoleUpdateDto dto);
        Task<bool> DeleteRoleAsync(long id);
    }
}
