using Fundbae.Domain.DTO;
using Fundbae.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Domain
{
    public enum AccountType
    {
        Flex,
        Deluxe,
        Viva,
        Piggy,
        Supa
    }
    public class Account
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime LastModified { get; set; }


        public static Account CreateAccount(AccountCreationDTO account)
        {
            
            var newAccount = Utilities.MapTo<Account>(account);
            newAccount.DateRegistered = DateTime.Now;
            newAccount.LastModified = DateTime.Now;
            newAccount.AccountNumber = GenerateAccountNumber();
            newAccount.AccountName = account.AccountName;
            return newAccount;
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
