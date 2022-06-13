using Fundbae.Domain;
using Fundbae.Domain.Context;
using Fundbae.Helpers;
using Fundbae.Services.Contracts;
using Fundbae.Services.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Repositories
{
    public  class InterestService : IInterestRateService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly AppSettings appSettings;
        private readonly IAccountService _accountService;

        public InterestService(AppDbContext context, IConfiguration configuration, IOptions<AppSettings> options, IAccountService accountService)
        {
            _context = context;
            _configuration = configuration;
            appSettings = options.Value;
            _accountService = accountService;
        }
        public async Task<Response<dynamic>> GetInterestRate(int accountNumber)
        {
            var checkAccount = _context.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();
            if (checkAccount == null)
            {
                return new Response<dynamic> { IsSuccess = false, Message = "Account not found"};

            }
            var accountBalance = checkAccount.AccountBalance;
            if (accountBalance < 20000)
            {
                return new Response<dynamic> { IsSuccess = false, Message = "AccountBlance lower than N20,000" };
            }

            if (checkAccount.AccountType == Domain.AccountType.Flex)
            {
                var rate = Convert.ToDecimal(2.5);
                var interest = accountBalance * rate / 100;

                checkAccount.CreditAccount(interest);
                await UpdateAccountBalance(checkAccount);

                return new Response<dynamic> { IsSuccess = true, Payload = interest, Message = "interest generated" };
            }

            if (checkAccount.AccountType == Domain.AccountType.Deluxe)
            {
                var rate = Convert.ToDecimal(3.5);
                var interest = accountBalance * rate / 100;
                checkAccount.CreditAccount(interest);
                await UpdateAccountBalance(checkAccount);
                return new Response<dynamic> { IsSuccess = true, Payload = interest, Message = "interest generated" };
            }
            if (checkAccount.AccountType == Domain.AccountType.Viva)
            {
                var rate = Convert.ToDecimal(6);
                var interest = accountBalance * rate / 100;
                checkAccount.CreditAccount(interest);
                await UpdateAccountBalance(checkAccount);
                return new Response<dynamic> { IsSuccess = true, Payload = interest, Message = "interest generated" };
            }

            if (checkAccount.AccountType == Domain.AccountType.Piggy)
            {
                var rate = Convert.ToDecimal(9.2);
                var interest = accountBalance * rate / 100;
                checkAccount.CreditAccount(interest);
                await UpdateAccountBalance(checkAccount);
                return new Response<dynamic> { IsSuccess = true, Payload = interest, Message = "interest generated" };
            }

            if (checkAccount.AccountType == Domain.AccountType.Supa)
            {
                var rate = Convert.ToDecimal(10.0);
                var interest = accountBalance * rate / 100;
                checkAccount.CreditAccount(interest);
                await UpdateAccountBalance(checkAccount);
                return new Response<dynamic> { IsSuccess = true, Payload = interest, Message = "interest generated" };
            }


            return new Response<dynamic> { IsSuccess = false, Message = "Oops! something went wrong" };



        }

        public async Task<Response<dynamic>> UpdateAccountBalance(Account account)
        {
            var res = _context.Update(account);
            if (res != null)
            {
                await _context.SaveChangesAsync();
                return Response<dynamic>.Send(true, "Credited Account Balance Successfully");
            }
            return Response<dynamic>.Send(false, "Oops something went wrong");

        }
    }
}
