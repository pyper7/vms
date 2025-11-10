using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.domain.Entities;

namespace vms.application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetByIdAsync(long id);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(long id);
    }
}
