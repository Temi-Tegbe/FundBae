﻿using Fundbae.Domain;
using Fundbae.Domain.Context;
using Fundbae.Domain.DTO;
using Fundbae.Helpers;
using Fundbae.Services.Contracts;
using Fundbae.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppSettings appSettings;

        public CustomerService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            appSettings = options.Value;
        }


        public async Task<Response<dynamic>> AddAsync(CustomerRegistrationDTO customer, ApplicationUser userInfo)
        {
            var existing = await GetCustomerDetails(customer.EmailAddress);
            if (existing != null)
            {
                return Response<dynamic>.Send(false, "User already exists", HttpStatusCode.Conflict);
            }
            var newCustomer = Customer.CreateCustomer(customer);
            await _context.Customers.AddAsync(
                new Customer
                {

                    EmailAddress = customer.EmailAddress,
                    DateRegistered = DateTime.Now,
                    PhoneNumber = customer.PhoneNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    User = userInfo
                    


                }
                );
            await _context.SaveChangesAsync();
            return Response<dynamic>.Send(true, "Added Successfully");
        }


        public async Task<Response<dynamic>> Login(CustomerLoginDTO login)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(login.EmailAddress);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT.Secret));
                var token = new JwtSecurityToken(
                    issuer: appSettings.JWT.ValidIssuer,
                    audience: appSettings.JWT.ValidAudience,
                    expires: DateTime.Now.AddMinutes(120),
                     claims: authClaims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                     );
                return Response<dynamic>.Send(true, "Logged in Suucessfully", payload: new { token = new JwtSecurityTokenHandler().WriteToken(token) });

            }

            return Response<dynamic>.Send(false, "Log in failed", HttpStatusCode.Unauthorized);
        }

        public async Task<Customer> GetCustomerDetails(string login)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(x => x.EmailAddress.ToLower() == login.ToLower());
            return result;
        }

        public async Task<PagedQueryResult<Customer>> GetAllCustomers(PagedQueryRequest request)
        {
            var allCustomers = _context.Customers;
            return allCustomers.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public static int GenerateAccountNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 10);

            if (true)
            {

            }
            return number;
        }
    }

}
