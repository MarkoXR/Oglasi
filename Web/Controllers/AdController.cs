using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oglasi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AdController : Controller
    {
        private readonly AdManagerDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public AdController(AdManagerDbContext dbContext, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _env = env;
        }

        public IActionResult Index(AdFilterModel filter)
        {
            ViewBag.Counties = CountiesDropdownList().Prepend(new SelectListItem("-Županija-", "0"));
            ViewBag.Categories = CategoriesDropdownList().Prepend(new SelectListItem("-Kategorija-", "0"));
            return View(filter);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAjax(AdFilterModel filter)
        {
            var query = _dbContext.Ads
                .Include(ad => ad.Category)
                .Include(ad => ad.County)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(ad => ad.Title.ToLower().Contains(filter.Title.ToLower()));
            if (filter.CategoryId != 0)
                query = query.Where(ad => ad.CategoryId == filter.CategoryId);
            if (filter.CountyId != 0)
                query = query.Where(ad => ad.CountyId == filter.CountyId);

            return PartialView("_IndexAds", await query?.ToListAsync());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var ad = _dbContext.Ads
                .Include(a => a.Category)
                .Include(a => a.County)
                .FirstOrDefault(a => a.Id == id);
            return View(ad);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create()
        {
            ViewBag.Counties = CountiesDropdownList();
            ViewBag.Categories = CategoriesDropdownList();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create(Ad ad)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Counties = CountiesDropdownList();
                ViewBag.Categories = CategoriesDropdownList();
                return View();
            }
            var adEntity = (await _dbContext.Ads.AddAsync(ad)).Entity;
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var user = await _dbContext.Users.Include(u => u.Ads).FirstOrDefaultAsync(u => u.Id == userId);
            user.Ads.Add(adEntity);
            await _dbContext.SaveChangesAsync();
            
            return RedirectToAction("Index", "Home");
        }

       
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await _dbContext.Ads.FindAsync(id);
            ViewBag.Counties = CountiesDropdownList();
            ViewBag.Categories = CategoriesDropdownList();
            return View(ad);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            var wwwroot = _env.WebRootPath;
            var filePath = Path.Combine(wwwroot, "images", file.FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Json(new { sucess = true, filePath = file.FileName });
        }

        private List<SelectListItem> CountiesDropdownList()
        {
            return _dbContext.Counties
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
        }

        private List<SelectListItem> CategoriesDropdownList()
        {
            return _dbContext.AdCategories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
        }
    }
}
