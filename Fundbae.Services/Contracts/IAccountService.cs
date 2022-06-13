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
        Task<PagedQueryResult<Account>> GetAllAccounts(PagedQueryRequest request);
        Task<PagedQueryResult<Account>> GetAllFlexAccounts(PagedQueryRequest request);
        Task<PagedQueryResult<Account>> GetAllSupaAccounts(PagedQueryRequest request);
        Task<PagedQueryResult<Account>> GetAllPiggyAccounts(PagedQueryRequest request);
        Task<PagedQueryResult<Account>> GetAllDeluxeAccounts(PagedQueryRequest request);
        Task<PagedQueryResult<Account>> GetAllVivaAccounts(PagedQueryRequest request);
        Task<Response<dynamic>> CreditAccount(int accountNumber, decimal amount);
        Task<Response<dynamic>> DebitAccount(int accountNumber, decimal amount);
        Task<PagedQueryResult<Account>> GetAllAccountsWithNoCustomer(PagedQueryRequest request);
    }
}
