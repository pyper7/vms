using vms.domain.Entities;
using vms.infrastructure;

namespace vms.presentation.Data
{
    public static class SeedData
    {
        public static void Initialize(VMSDbContext context)
        {
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var managerRole = context.Roles.FirstOrDefault(r => r.Name == "Manager");
            var employeeRole = context.Roles.FirstOrDefault(r => r.Name == "Receptionist");

            if (!context.Users.Any(u => u.Email == "admin@vms.com"))
            {

                var adminUser = new User
                {
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@vms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = adminRole
                };
                context.Users.Add(adminUser);
            }

            if (!context.Users.Any(u => u.Email == "manager@vms.com"))
            {
                var managerUser = new User
                {
                    FirstName = "Test",
                    LastName = "Manager",
                    Email = "manager@vms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                    Role = managerRole
                };
                context.Users.Add(managerUser);
            }

            if (!context.Users.Any(u => u.Email == "receptionist@vms.com"))
            {
                var employeeUser = new User
                {
                    FirstName = "Test",
                    LastName = "Receptionist",
                    Email = "receptionist@vms.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Receptionist123!"),
                    Role = employeeRole
                };
                context.Users.Add(employeeUser);
            }

            context.SaveChanges();
        }
    }
}
