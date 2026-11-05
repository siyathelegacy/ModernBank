using ModernBank.Contracts.DTOs.AccountManagement;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace ModernBank.Http_Client.Services.AccountService
{
    public class AccountService(HttpClient client) : IAccountService
    {
        public async Task<AccountDto> GetAcountInfo(AccountDto dto)
        {
            var response = await client.GetFromJsonAsync<AccountDto>("", dto);
            return response is not null ? response : new AccountDto();
        }
    }
}
