using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oglasi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("/api/posts")]
    [ApiController]
    public class PostApiController : Controller
    {
        private readonly AdManagerDbContext _dbContext;

        public PostApiController(AdManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Posts.ToListAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            return post is null ? NotFound() : Ok(post);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            var result = await _dbContext.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return Ok(result.Entity);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Post post)
        {
            var dbPost = await _dbContext.Posts.FindAsync(id);
            if (dbPost is null)
                return NotFound();

            dbPost.Title = post.Title;
            dbPost.Message = post.Message;
            dbPost.PostDate = post.PostDate;

            await _dbContext.SaveChangesAsync();
            dbPost = await _dbContext.Posts.FindAsync(id);
            return dbPost is null ? NotFound() : Ok(dbPost);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var dbPost = await _dbContext.Posts.FindAsync(id);
            if (dbPost is null)
                return NotFound();

            _dbContext.Posts.Remove(dbPost);
            int ok = await _dbContext.SaveChangesAsync();

            if (ok == 0 || await _dbContext.Posts.FindAsync(id) is not null)
                return StatusCode(500);

            return Ok(dbPost);
        }
    }
}
