using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankAccounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BankAccounts.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BankAccountsContext _context;

    public HomeController(ILogger<HomeController> logger, BankAccountsContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        HttpContext.Session.SetString("ValidationSummary", "RegisterForm");
        if (ModelState.IsValid)
        {

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);

            _context.SaveChanges();


            HttpContext.Session.SetInt32("UserId", user.UserId);
            return RedirectToAction("Account", new { id = user.UserId });
        }
        else
        {
            return View("Index");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [SessionCheck]
    [HttpGet("accounts/{id}")]
    public IActionResult Account(int id)
    {
        int? userid = HttpContext.Session.GetInt32("UserId");
        if (userid != id)
        {
            return RedirectToAction("Account", new { id = userid });
        }
        User? user = _context.Users
            .Include(u => u.Transactions)
            .FirstOrDefault(u => u.UserId == id);
        Transaction? transaction = new Transaction();
        AccountViewModel? viewModel = new AccountViewModel()
        {
            User = user,
            Transaction = transaction
        };
        return View(viewModel);
    }



    [HttpPost("login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        HttpContext.Session.SetString("ValidationSummary", "LoginForm");
        if (ModelState.IsValid)
        {

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
            if (userInDb == null)
            {

                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
            var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
            if (result == 0)
            {

                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("Account", "Home", new { id = userInDb.UserId });
        }
        else
        {
            return View("Index");
        }
    }

    [HttpPost("transaction")]
    public IActionResult HandleTransaction(Transaction transaction)
    {
        HttpContext.Session.SetString("ValidationSummary", "TransactionForm");
        int? userid = HttpContext.Session.GetInt32("UserId");
        Console.WriteLine(transaction.Amount);
        if (ModelState.IsValid)
        {
            User? user = _context.Users
                .Include(u => u.Transactions)
                .FirstOrDefault(u => u.UserId == userid);

            if (transaction.Amount < 0)
            {
                if (Math.Abs(transaction.Amount) > user.Balance)
                {
                    ModelState.AddModelError("Amount", "Insufficient Funds");
                    return RedirectToAction("Account", new { id = userid });
                }
            }


            transaction.UserId = (int)user.UserId;
            user.Balance += transaction.Amount;
            _context.Add(transaction);
            _context.SaveChanges();
            return RedirectToAction("Account", new { id = userid });
        }
        else
        {
            return RedirectToAction("Account", new { id = userid });
        }
    }
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
