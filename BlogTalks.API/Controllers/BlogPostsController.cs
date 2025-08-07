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
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetByIdRequest(id));
            return Ok(response);
        }

        // POST api/<BlogPostsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateRequest request)
        {
            var result = _mediator.Send(new UpdateRequest(id, request.Title, request.Text, request.Tags));
            return Ok(result.Result);
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var blogPost = _mediator.Send(new DeleteRequest(id));
            return NoContent();
        }
    }
}
