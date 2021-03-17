using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Steps.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steps.Controllers
{
    public class HomeController : Controller
    {
        private readonly StepsContext _context;

        public HomeController(StepsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/privacy-policy")]
        public IActionResult Policy()
        {
            return View();
        }

        [HttpGet("/about")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Employees()
        {
            if (HttpContext.Request.Cookies["User"] == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Fitspos");
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Employees([Bind("Id,Username,Password,IsLogged")] string Username, string Password, Models.Login login)
        {
            if(ModelState.IsValid)
            {
                var user = await _context.Steps_Login
                .FirstOrDefaultAsync(m => m.Username == Username);
                if (Username == user.Username && Password == user.Password)
                {
                    return RedirectToAction("Index", "Fitspos", new { id = user.Username });
                }
                ModelState.AddModelError("Password", "Username and password doesn't match");
                return View(login);
            }
            return View(login);
        }

        [HttpPost("/search")]
        public async Task<IActionResult> Search(string search)
        {
            ViewData["searchStr"] = search;

            var articles = from a in _context.Steps_Fitspos
                           select a;

            var count = _context.Steps_Fitspos
                .Select(o => o.Id)
                .Count();

            if (!String.IsNullOrEmpty(search))
            {
                articles = articles.Where(s => s.Post.Contains(search));
                count = _context.Steps_Fitspos
                    .Where(s => s.Post.Contains(search))
                    .Select(o => o.Id)
                    .Count();
            }

            if(count == 0)
            {
                ViewBag.SearchResults = "No results were found";
            } else if (count == 1) {
                ViewBag.SearchResults = "1 result found";
            } else
            {
                ViewBag.SearchResults = count + " results found";
            }

            return View(await articles.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Email([Bind("EmailId,EmailStr")] Models.Email emailMod, string email)
        {
            if(ModelState.IsValid)
            {
                if (email != null)
                {
                    var count = _context.Steps_Emails
                        .Where(s => s.EmailStr.Equals(email.ToString()))
                        .Select(o => o.EmailId)
                        .Count();

                    if (count == 0)
                    {
                        Models.Email emailNew = new Models.Email
                        {
                            EmailId = emailMod.EmailId,
                            EmailStr = email
                        };
                        _context.Add(emailNew);
                        await _context.SaveChangesAsync();
                        ViewBag.State = "Success!";
                        ViewBag.StateMessage = "Your e-mail has successfully been added. Check your inbox!";
                        return View();
                    }
                    else
                    {
                        ViewBag.State = "Hmm, something went wrong...";
                        ViewBag.StateMessage = "You need to enter a valid email that isn't already on the list. Try again!";
                        return View();
                    }
                }
            }
            ViewBag.State = "Hmm, something went wrong...";
            ViewBag.StateMessage = "You need to enter a valid email that isn't already on the list. Try again!";
            return View();

        }
    }
}
