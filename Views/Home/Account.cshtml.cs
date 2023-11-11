using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BankAccounts.Views.Home
{
    public class Account : PageModel
    {
        private readonly ILogger<Account> _logger;

        public Account(ILogger<Account> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}