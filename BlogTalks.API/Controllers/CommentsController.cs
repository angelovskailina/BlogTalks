using BlogTalks.Application.Comments.Commands;
using BlogTalks.Application.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogTalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentsController(IMediator mediator) => _mediator = mediator;


        // GET: api/<CommentsController>
        [HttpGet("blogpost/{id}/comments")]
        public async Task<ActionResult> GetByBlogPostId([FromRoute] int id)
        {
            var comments = await _mediator.Send(new GetAllByBlogPostIdRequest(id));
            return Ok(comments);
        }


        // GET api/<CommentsController>/5
        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var comment = await _mediator.Send(new GetByIdRequest(id));
            return Ok(comment);
        }

        // GET api/<CommentsController>/5
        //[HttpGet]
        //public async Task<ActionResult> GetAll()
        //{

        //    var comment = await _mediator.Send(new GetAllRe;

        //    return Ok(comment);

        //}

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateRequest request)
        {
            var response = _mediator.Send(new UpdateRequest(id, request.Text));
            if (response == null)
            {
                return NotFound();
            }
            return NoContent();

        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteRequest(id));
            if (response == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
