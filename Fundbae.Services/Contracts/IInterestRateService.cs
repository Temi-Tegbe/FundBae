using Fundbae.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Contracts
{
    public interface IInterestRateService
    {
        Task<Response<dynamic>> GetInterestRate(int accountNumber);
    }
}
