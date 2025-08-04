using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Application.BlogPosts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//get post bad request
//put i delete no conctent 
namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BlogPostsController(IMediator mediator) => _mediator = mediator;

        // GET: api/<BlogPostsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var blogPosts = await _mediator.Send(new GetAllRequest());
            return Ok(blogPosts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] GetByIdRequest request)
        {
            try
            {
                var blogpost = await _mediator.Send(request);
                return Ok(blogpost);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BlogPostsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddResponse response)
        {
            await _mediator.Send(new AddRequest(response));
            return Ok(response);
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateRequest request)
        {
            try
            {
                var updatedBlogpost = _mediator.Send(request);
                return Ok(updatedBlogpost.Result);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var blogPost = _mediator.Send(new DeleteRequest(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
