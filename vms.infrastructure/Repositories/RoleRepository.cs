using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.application.Interfaces.Repositories;
using vms.domain.Entities;

namespace vms.infrastructure.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly VMSDbContext _context;
        public RoleRepository(VMSDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }
        public async Task<Role> GetByIdAsync(long id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                role.IsDeleted = true;
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
