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
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)          
                .FirstOrDefaultAsync(u => u.Email == username);
        }
        public async Task AddRevokedTokenAsync(RevokedToken token)
        {
            _context.RevokedTokens.Add(token);
            await _context.SaveChangesAsync();
        }

    }
}
