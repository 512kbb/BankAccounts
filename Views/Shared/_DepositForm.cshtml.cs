using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BankAccounts.Views.Shared
{
    public class _DepositForm : PageModel
    {
        private readonly ILogger<_DepositForm> _logger;

        public _DepositForm(ILogger<_DepositForm> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}