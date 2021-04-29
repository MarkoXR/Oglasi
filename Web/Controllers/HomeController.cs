using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Oglasi.Model;
using Oglasi.Models;
using System.Diagnostics;

namespace Oglasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdManagerDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AdManagerDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ad");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
