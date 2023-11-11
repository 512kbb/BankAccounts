using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts.Models
{
    public class AccountViewModel
    {
        public User? User { get; set; }
        public Transaction? Transaction { get; set; }
    }
}