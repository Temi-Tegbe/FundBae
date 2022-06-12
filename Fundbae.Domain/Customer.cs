using Fundbae.Domain.Context;
using Fundbae.Domain.DTO;
using Fundbae.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Fundbae.Domain
{
   
    public class Customer
    {
        
        [Key]
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long UserId { get; set; }

        public ApplicationUser User { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime LastModified { get; set; }
        public Account Accounts { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }




        public static Customer CreateCustomer(CustomerRegistrationDTO customer)
        {
            var newCustomer = Utilities.MapTo<Customer>(customer);
            newCustomer.DateRegistered = DateTime.Now;
            newCustomer.LastModified = DateTime.Now;
            newCustomer.Accounts.AccountType = AccountType.Flex;
            newCustomer.Accounts.AccountNumber = GenerateAccountNumber();
            return newCustomer;
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


