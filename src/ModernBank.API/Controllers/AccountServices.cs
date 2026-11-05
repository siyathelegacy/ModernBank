using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernBank.Application.Repositories;
using ModernBank.Contracts.DTOs.AccountManagement;
using UserCustomerType = ModernBank.Contracts.DTOs.UserManagement.CustomerType;
using UserIdentityDocumentType = ModernBank.Contracts.DTOs.UserManagement.IdentityDocumentType;

namespace ModernBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountServices(IAccountRepository accounts) : ControllerBase
    {
        [Authorize(Roles ="Admin")]
        [HttpGet("GetUserAccount")]
        public async Task<IActionResult> UserAccount([FromQuery] string email, CancellationToken cancellationToken)
        {
            var account = await accounts.GetByEmailAsync(email, cancellationToken);
            if (account is null)
            {
                return NotFound();
            }

            return Ok(new AccountDto
            {
                Id = account.UserId,
                CustomerType = (UserCustomerType?)account.CustomerType,
                FullName = account.FullName,
                Email = account.Email,
                MobileNumber = account.MobileNumber,
                IdentityType = (UserIdentityDocumentType?)account.IdentityType,
                IdentityNumber = account.IdentityNumber,
                OrganizationName = account.OrganizationName,
                TaxReference = account.TaxReference
            });
        }
    }
}
