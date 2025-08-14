using BlogTalks.Application.Comments.Commands;
using BlogTalks.Application.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        public CommentsController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("blogpost/{id}/comments")]
        public async Task<ActionResult> GetByBlogPostId([FromRoute] int id)
        {
            var comments = await _mediator.Send(new GetAllByBlogPostIdRequest(id));
            return Ok(comments);
        }

        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var comment = await _mediator.Send(new GetByIdRequest(id));
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRequest request)
        {
            _logger.LogInformation("----- New created comment  received.");

            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateRequest request)
        {
            var response = _mediator.Send(new UpdateRequest(id, request.Text));
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogInformation("----- Deleted comment.");

            var response = await _mediator.Send(new DeleteRequest(id));
            return NoContent();
        }
    }
}
