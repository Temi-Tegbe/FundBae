using Fundbae.Domain;
using Fundbae.Domain.Context;
using Fundbae.Domain.DTO;
using Fundbae.Helpers;
using Fundbae.Services.Contracts;
using Fundbae.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Repositories
{
    public class AccountService : IAccountService
    {
        public readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppSettings appSettings;

        public AccountService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IOptions<AppSettings> options)
        {
              _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            appSettings = options.Value;
            }
        public async Task<Response<dynamic>> AddAsync(AccountCreationDTO account)
        {
            var checkAccounts = _context.Accounts.Where(x => x.CustomerId == account.CustomerId).Count();
            if (checkAccounts > 5)
            {
                return Response<dynamic>.Send(false, "You can only create a maximum of 5 accounts");
            }
            var newAccount = Account.CreateAccount(account);
            var checkNumberOfAccounts = 
            await _context.Accounts.AddAsync(
                new Account
                {
                    AccountType = account.AccountType,
                    AccountBalance = 0

                }
                );
            await _context.SaveChangesAsync();
            return Response<dynamic>.Send(true, "Account Created Successfully");
        }



        public async Task<PagedQueryResult<Account>> GetAllAccounts(PagedQueryRequest request)
        {
            var allAccounts = _context.Accounts;
            return allAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public async Task<PagedQueryResult<Account>> GetAllFlexAccounts(PagedQueryRequest request)
        {
            var flexAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Flex);
            return flexAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public async Task<PagedQueryResult<Account>> GetAllSupaAccounts(PagedQueryRequest request)
        {
            var supaAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Supa);
            return supaAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public async Task<PagedQueryResult<Account>> GetAllPiggyAccounts(PagedQueryRequest request)
        {
            var piggyAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Piggy);
            return piggyAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }
        public async Task<PagedQueryResult<Account>> GetAllDeluxeAccounts(PagedQueryRequest request)
        {
            var supaAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Deluxe);
            return supaAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public async Task<PagedQueryResult<Account>> GetAllVivaAccounts(PagedQueryRequest request)
        {
            var vivaAccounts = _context.Accounts.Where(x => x.AccountType == AccountType.Viva);
            return vivaAccounts.ToPagedResult(request.PageNumber, request.PageSize);
        }


        public async Task<Response<dynamic>> CreditAccount(int accountNumber, decimal amount)
        {
            if (amount <= 0)
            {
                return Response<dynamic>.Send(false, "amount cannot be less than or equal to zero", System.Net.HttpStatusCode.BadRequest);
            }
            var findAccount =  _context.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
            if (findAccount != null)
            {
                findAccount.CreditAccount(amount);
                await UpdateAccountBalance(findAccount);
                return Response<dynamic>.Send(true, "Account credited Successfully");
            }
            return Response<dynamic>.Send(false, "Oops something went wrong");
        }

        public async Task<Response<dynamic>> DebitAccount(int accountNumber, decimal amount)
        {
            if (amount <= 0)
            {
                return Response<dynamic>.Send(false, "amount cannot be less than or equal to zero", System.Net.HttpStatusCode.BadRequest);
            }
            var findAccount = _context.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
            if (findAccount != null)
            {
                findAccount.DebitAccount(amount);
                await UpdateAccountBalance(findAccount);
                return Response<dynamic>.Send(true, "Account debited Successfully");
            }
            return Response<dynamic>.Send(false, "Oops something went wrong");
        }



        public async Task<Response<dynamic>> UpdateAccountBalance(Account account)
        {
            var res =  _context.Update(account);
            if (res != null)
            {
                await  _context.SaveChangesAsync();
                return Response<dynamic>.Send(true, "Credited Account Balance Successfully");
            }
            return Response<dynamic>.Send(false, "Oops something went wrong");

        }
    }
}
