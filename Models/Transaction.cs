using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccounts.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [CheckNotZero]
        [Display(Name = "Deposit/Withdraw: ")]
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        
    }
}