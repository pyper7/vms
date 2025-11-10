using Microsoft.EntityFrameworkCore;
using vms.domain.Entities;



namespace vms.infrastructure
{
    public class VMSDbContext : DbContext
    {
        public VMSDbContext(DbContextOptions<VMSDbContext> options): base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
    }
}
