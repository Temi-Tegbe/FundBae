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
            var newAccount = Account.CreateAccount(account);
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
    }
}
