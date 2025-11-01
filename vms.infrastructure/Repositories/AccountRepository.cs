using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.application.Interfaces.Repositories;

namespace vms.infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly VMSDbContext _context;
        public AccountRepository(VMSDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ValidateUser(string username, string password)
        {
            return _context.Users.Any(u => u.Email == username && u.PasswordHash == password);
        }
    }
}
