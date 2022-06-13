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
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private readonly AppSettings _appsettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CustomerController(ICustomerService customerService, IOptions<AppSettings> appsettings, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
            {
            _appsettings = appsettings.Value;
            _customerService = customerService;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRegistrationDTO customerRegistrationDTO)
        {
          var create =  await _customerService.AddAsync(customerRegistrationDTO);
            
            return Ok(create);
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginDTO login)
        {
            var customerLogin = await _customerService.Login(login);
            if (customerLogin.IsSuccess)
            {
                return Ok(customerLogin);
            }

            return BadRequest("Login failed");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers(PagedQueryRequest request)
        {
            var allCustomers = await _customerService.GetAllCustomers(request);
            return Ok(allCustomers);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCustomersWithZeroBalance(PagedQueryRequest request)
        {
            var zeroBalance = await _customerService.GetAllCustomersWithZeroBalance(request);
            return Ok(zeroBalance);
        }


    }
}
