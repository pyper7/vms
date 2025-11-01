using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vms.application.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> ValidateUser(string username, string password);
    }
}
