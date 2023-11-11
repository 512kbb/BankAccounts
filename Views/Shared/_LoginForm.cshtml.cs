using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BankAccounts.Views.Shared
{
    public class _LoginForm : PageModel
    {
        private readonly ILogger<_LoginForm> _logger;

        public _LoginForm(ILogger<_LoginForm> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}