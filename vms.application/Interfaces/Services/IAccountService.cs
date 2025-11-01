using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.shared.DTO;

namespace vms.application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<LoginResponseDto> Login(string username, string password);
    }
}
