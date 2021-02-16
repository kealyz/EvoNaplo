using EvoNaplo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Services
{
    public class LoginService
    {
        private readonly UserActionService _userActionService;

        public LoginService(UserActionService userActionService)
        {
            _userActionService = userActionService;
        }

        public async Task<string> SignInUser(LoginDTO loginDto)
        {
            return await _userActionService.SignInUser(loginDto);
        }
    }
}
