using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Domain.DTO
{
    public class AccountCreationDTO
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public Guid CustomerId { get; set; }
    }
}
