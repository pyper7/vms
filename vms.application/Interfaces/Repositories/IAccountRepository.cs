using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.domain.Entities;

namespace vms.application.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> ValidateUser(string username, string password);
        Task<User> GetByUsernameAsync(string username);
        Task AddRevokedTokenAsync(RevokedToken token);

    }
}
