using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email is required");
            }

            BankAccountsContext? _context = (validationContext?.GetService(typeof(BankAccountsContext)) as BankAccountsContext);

            if(_context?.Users.Any(u => u.Email == value.ToString()) == true)
            {
                return new ValidationResult("Email already in use!");
            }
            
            return ValidationResult.Success;
        }
    }
}