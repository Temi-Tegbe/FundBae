using Fundbae.Domain;
using Fundbae.Domain.DTO;
using Fundbae.Helpers;
using Fundbae.Services.Contracts;
using Fundbae.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FundBae.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly AppSettings _appsettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(IAccountService accountService, IOptions<AppSettings> appsettings, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _accountService = accountService;
            _appsettings = appsettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountCreationDTO accountCreationDTO)
        {
            var create = await _accountService.AddAsync(accountCreationDTO);

            return Ok(create);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts(PagedQueryRequest request)
        {
            var allAccounts = await _accountService.GetAllAccounts(request);
            return Ok(allAccounts);
        }

    }
}
