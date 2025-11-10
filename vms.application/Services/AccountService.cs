using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using vms.application.Interfaces.Repositories;
using vms.application.Interfaces.Services;
using vms.domain.Entities;
using vms.shared.DTO;

namespace vms.application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtService _jwtService;

        public AccountService(IAccountRepository accountRepository, JwtService jwtService)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> Login(string username, string password)
        {
            var user = await _accountRepository.GetByUsernameAsync(username);
            if (user == null)
                return null;

            // Validate password (using BCrypt)
            var passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!passwordValid)
                return null;

            // Generate JWT
            var (token, expiresAt) = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Email,
                Role = user.Role?.Name ?? "User",
                ExpiresAt = expiresAt
            };
        }

        public async Task LogoutAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Get the expiry date from the token
            var expiresAt = jwtToken.ValidTo;

            var revoked = new RevokedToken
            {
                Token = token,
                RevokedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            await _accountRepository.AddRevokedTokenAsync(revoked);
        }


    }

}
