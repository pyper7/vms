using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.application.Interfaces.Repositories;
using vms.application.Interfaces.Services;
using vms.shared.DTO;

namespace vms.application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task<LoginResponseDto> Login(string username, string password)
        {
            var isValidUser = await _accountRepository.ValidateUser(username, password);
            if (!isValidUser)
            {
                return new LoginResponseDto();
            }
            return new LoginResponseDto();

        }
    }
}
