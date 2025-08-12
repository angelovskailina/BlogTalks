using BlogTalks.Application.Users.Commands;
using BlogTalks.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BlogTalks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (BlogTalksException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (BlogTalksException ex)
            {
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
        }
    }
}
