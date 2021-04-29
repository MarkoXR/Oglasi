using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oglasi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly AdManagerDbContext _dbContext;

        public PostController(AdManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var posts = _dbContext.Posts
                .OrderByDescending(p => p.PostDate)
                .ToList();
            return View(posts);
        }

        public async Task<IActionResult> GetPostsAjax()
        {
            var posts = await _dbContext.Posts
               .OrderByDescending(p => p.PostDate)
               .ToListAsync();
            return PartialView("_PostsAjax", posts);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post is null)
                return NotFound();
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post is null)
                return NotFound();
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
            return await GetPostsAjax();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            return View(post);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post is null)
                return NotFound();
            var ok = await TryUpdateModelAsync(post);
            if (!ok)
                return StatusCode(501);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
