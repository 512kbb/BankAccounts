using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts.Models
{
    public class CheckNotZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((double)value == 0)
            {
                return new ValidationResult("Amount must not be zero");
            }
            return ValidationResult.Success;
        }
    }
}