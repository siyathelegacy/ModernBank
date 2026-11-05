using ModernBank.Contracts.DTOs.AccountManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernBank.Http_Client.Services.AccountService
{
    public interface IAccountService
    {
        Task<AccountDto> GetAcountInfo(AccountDto dto);
    }
}
