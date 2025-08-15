using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Application.BlogPosts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTalks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public BlogPostsController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<GetAllResponse>> Get([FromQuery] int? pageNumber, int? pageSize, string? searchWord, string? tag)
        {
            var request = new GetAllRequest
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 10,
                SearchWord = searchWord,
                Tag = tag,
            };
            var list = await _mediator.Send(request);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetByIdRequest(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRequest request)
        {
            _logger.LogInformation("----- New created blogpost received.");
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateRequest request)
        {
            var result = _mediator.Send(new UpdateRequest(id, request.Title, request.Text, request.Tags));
            return Ok(result.Result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("----- Deleted blogpost.");

            var blogPost = _mediator.Send(new DeleteRequest(id));
            return NoContent();
        }
    }
}
