using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vms.application.Interfaces.Services;
using vms.shared.DTO;

namespace vms.presentation.Controllers
{
    [Route("api/v1/[controller]")]
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
    }
}
