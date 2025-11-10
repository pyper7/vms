using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vms.application.Interfaces.Services;
using vms.application.Interfaces.Repositories;
using vms.domain.Entities;
using vms.shared.DTO.Roles;

namespace vms.application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return roles.Select(r => new RoleResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                DateCreated = r.DateCreated,
                DateUpdated = r.DateUpdated
            });
        }

        public async Task<RoleResponseDto> GetRoleByIdAsync(long id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return null;

            return new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                DateCreated = role.DateCreated,
                DateUpdated = role.DateUpdated
            };
        }

        public async Task<RoleResponseDto> CreateRoleAsync(RoleCreateDto dto)
        {
            var role = new Role
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _roleRepository.AddAsync(role);

            return new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                DateCreated = role.DateCreated
            };
        }

        public async Task<RoleResponseDto> UpdateRoleAsync(long id, RoleUpdateDto dto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
                return null;

            existingRole.Name = dto.Name;
            existingRole.Description = dto.Description;
            existingRole.DateUpdated = DateTime.UtcNow;

            await _roleRepository.UpdateAsync(existingRole);

            return new RoleResponseDto
            {
                Id = existingRole.Id,
                Name = existingRole.Name,
                Description = existingRole.Description,
                DateCreated = existingRole.DateCreated,
                DateUpdated = existingRole.DateUpdated
            };
        }

        public async Task<bool> DeleteRoleAsync(long id)
        {
            var existing = await _roleRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _roleRepository.DeleteAsync(id);
            return true;
        }
    }
}
