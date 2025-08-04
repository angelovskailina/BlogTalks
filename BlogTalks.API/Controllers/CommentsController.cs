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
        [HttpGet("blogpost/{id}/comments", Name = "GetCommentsByBlogPostId")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            try
            {
                var comments = await _mediator.Send(new GetAllByBlogPostIdRequest(id));
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }// custom ex vo entities 
        }


        // GET api/<CommentsController>/5
        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult> Get([FromRoute] GetByIdRequest request)
        {
            try
            {
                var comment = await _mediator.Send(new GetByIdRequest(request.id));

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddResponse response)
        {
            await _mediator.Send(new AddRequest(response));
            return Ok(response);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateRequest request)
        {
            try
            {
                var updatedComment = _mediator.Send(request);
                return Ok(updatedComment.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var comment = await _mediator.Send(new DeleteRequest(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
