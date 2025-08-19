using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace BlogTalks.Tests.UnitTests.BlogPosts
{
    public class AddHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogpostRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly AddHandler _handler;

        public AddHandlerTests()
        {
            _blogpostRepositoryMock = new Mock<IBlogPostRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _handler = new AddHandler(_blogpostRepositoryMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact] 
        public async Task Handle_ShouldCreateBlogPost_WhenRequestIsValid()
        {

            var userId = "124";

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var request = new AddRequest(
                "Test Title",
                "Test Content",
                new List<string> { "tag1", "tag2" }
            );

            _blogpostRepositoryMock.Setup(r => r.Add(It.IsAny<BlogPost>()))
                .Callback<BlogPost>(b => b.Id = 123);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(123, result.Id);
            _blogpostRepositoryMock.Verify(x => x.Add(It.Is<BlogPost>(b =>
                b.Title == request.Title &&
                b.Text == request.Text &&
                b.CreatedBy == int.Parse(userId) &&
                b.Tags == request.Tags)), Times.Once);
        }

        [Fact] 
        public async Task Handle_ShouldThrowException_WhenUserIdClaimIsMissing()
        {
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var request = new AddRequest(
                "Title without user",
                "Content without user",
                new List<string> { "tag1", "tag2" }
            );

           await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }
    }
}
