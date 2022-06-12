using Fundbae.Domain;
using Fundbae.Domain.DTO;
using Fundbae.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Contracts
{
    public interface IAccountService
    {
        Task<Response<dynamic>> AddAsync(AccountCreationDTO account);
    }
}
