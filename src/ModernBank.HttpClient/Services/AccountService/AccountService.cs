using ModernBank.Contracts.DTOs.AccountManagement;
using System.Net.Http.Json;

namespace ModernBank.Http_Client.Services.AccountService
{
    public class AccountService(HttpClient client) : IAccountService
    {
        public async Task<AccountDto> GetAcountInfo(AccountDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var email = Uri.EscapeDataString(dto.Email);
            var response = await client.GetFromJsonAsync<AccountDto>(
                $"api/AccountServices/GetUserAccount?email={email}");

            return response ?? throw new InvalidOperationException(
                "The account service returned an empty response.");
        }
    }
}
