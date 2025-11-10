using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vms.application.Interfaces.Services;
using vms.shared.DTO;

namespace vms.presentation.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _accountService.Login(loginRequest.Username, loginRequest.Password);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Get token from the Authorization header
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return BadRequest("Missing or invalid Authorization header.");

            var token = authHeader.Substring("Bearer ".Length).Trim();

            await _accountService.LogoutAsync(token);

            return Ok(new { message = "Logout successful. Token revoked." });
        }

    }
}
