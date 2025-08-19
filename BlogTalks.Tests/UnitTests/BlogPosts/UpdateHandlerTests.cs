using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using BlogTalks.Domain.Exceptions;

namespace BlogTalks.Tests.UnitTests.BlogPosts
{
    public class UpdateHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogpostRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly UpdateHandler _handler;

        public UpdateHandlerTests()
        {
            _blogpostRepositoryMock = new Mock<IBlogPostRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _handler = new UpdateHandler(_blogpostRepositoryMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateBlogPost_WhenRequestIsValid()
        {
            var userId = "124";
            var blogPostId = 1;

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var existingBlogPost = new BlogPost
            {
                Id = blogPostId,
                Title = "Old Title",
                Text = "Old Content",
                CreatedBy = int.Parse(userId),
                Tags = new List<string> { "oldtag" }
            };
            _blogpostRepositoryMock.Setup(r => r.GetById(blogPostId)).Returns(existingBlogPost);

            _blogpostRepositoryMock.Setup(r => r.Update(It.IsAny<BlogPost>()));

            var request = new UpdateRequest(
                blogPostId,
                "New Title",
                "New Content",
                new List<string> { "newtag1", "newtag2" }
            );
            var result = await new UpdateHandler(_blogpostRepositoryMock.Object, _httpContextAccessorMock.Object)
                .Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(blogPostId, result.Id);
        }

        [Fact] 
        public async Task Handle_ShouldThrowBlogTalksException_WhenUserTriesToUpdateOtherUsersPost()
        {
            var userId = "124";

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            var existingBlogPost = new BlogPost
            {
                Id = 1,
                Title = "Old Title",
                Text = "Old Content",
                CreatedBy = 999,  
                Tags = new List<string> { "tag" }
            };

            _blogpostRepositoryMock.Setup(r => r.GetById(existingBlogPost.Id)).Returns(existingBlogPost);

            var request = new UpdateRequest(
                existingBlogPost.Id,
                "New Title",
                "New Content",
                new List<string> { "tag1", "tag2" }
            );


            await Assert.ThrowsAsync<BlogTalksException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }
    }
}
