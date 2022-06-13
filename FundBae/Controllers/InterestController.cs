using Fundbae.Services.Contracts;
using Fundbae.Services.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FundBae.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterestController : ControllerBase
    {

        private readonly IInterestRateService _interestRateService;
        private readonly AppSettings _appsettings;
        
        
        public InterestController(IInterestRateService interestRateService, IOptions<AppSettings> appsettings)
        {
            _appsettings = appsettings.Value;
            _interestRateService = interestRateService;

        }








        [HttpPost]
        public async Task<IActionResult> GetInterest(int accountNumber)
        {
            var interest = _interestRateService.GetInterestRate(accountNumber);
            return Ok(interest);
        }

    }
}
