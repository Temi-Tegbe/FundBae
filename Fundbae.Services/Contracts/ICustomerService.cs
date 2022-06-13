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
    public interface ICustomerService
    {
        Task<Response<dynamic>> AddAsync(CustomerRegistrationDTO customer);
        //Task<Response<dynamic>> UpdateAsync(CustomerRegistrationDTO customer, ApplicationUser userInfo);
        Task<Response<dynamic>> Login(CustomerLoginDTO login);
         Task<Customer> GetCustomerDetails(string login);
        Task<PagedQueryResult<Customer>> GetAllCustomers(PagedQueryRequest request);
        Task<PagedQueryResult<Customer>> GetAllCustomersWithZeroBalance(PagedQueryRequest request);

    }
}
