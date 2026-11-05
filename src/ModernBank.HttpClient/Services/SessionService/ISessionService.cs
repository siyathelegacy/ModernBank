using System;
using System.Collections.Generic;
using System.Text;

namespace ModernBank.Http_Client.Services.SessionService
{
    public interface ISessionService
    {
        Task StartSessionAsync(string token, string userName, string role);
        Task EndSessionAsync();
        Task<string?> GetTokenAsync();
        Task<string?> GetRoleAsync();
        Task<string?> GetUserNameAsync();
    }
}
}
