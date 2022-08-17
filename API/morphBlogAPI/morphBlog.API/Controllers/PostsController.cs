using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using morphBlog.API.Models.DTO;
using morphBlog.API.Models.Entities;
using MorphBlog.API.Data;

namespace morphBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly MorphBlogDbContext dbContext;

        public PostsController(MorphBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
           var posts =  await dbContext.Posts.ToListAsync();

           return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            
            if(post != null)
            {
                return Ok(post);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            // convert DTO to Entity
            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Author = addPostRequest.Author,
                FeaturedImage = addPostRequest.FeaturedImage,
                PublishDate = addPostRequest.PublishDate,
                UpdateDate = addPostRequest.UpdateDate,
                Summary = addPostRequest.Summary,
                UrlHandle = addPostRequest.UrlHandle,
                Visible = addPostRequest.Visible,
            };

            post.Id = Guid.NewGuid();
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {

            // check if post exists
           var existingPost = await dbContext.Posts.FindAsync(id);

            if(existingPost != null)
            {
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Author = updatePostRequest.Author;
                existingPost.FeaturedImage = updatePostRequest.FeaturedImage;
                existingPost.PublishDate = updatePostRequest.PublishDate;
                existingPost.UpdateDate = updatePostRequest.UpdateDate;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;
                existingPost.Visible = updatePostRequest.Visible;

                await dbContext.SaveChangesAsync();

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);

            if (existingPost != null)
            {
                dbContext.Remove(existingPost);
                await dbContext.SaveChangesAsync(true);

                return Ok(existingPost);
            }

            return NotFound();
;       }
    }
}
