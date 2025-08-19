using BlogTalks.Application.BlogPosts.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Moq;

namespace BlogTalks.Tests.UnitTests.BlogPosts
{
    public class DeleteHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        private readonly DeleteHandler _handler;

        public DeleteHandlerTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            _handler = new DeleteHandler(_blogPostRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnException_WhenBlogpostDoestNotExist()
        {
            var blogpostId = 5;
            var query = new DeleteRequest(blogpostId);

            await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_ReturnBlogpostResponse_WhenBlogpostExist()
        {
            var blogpostId = 5;

            var blogpost = new BlogPost { Id = blogpostId, Title = "Test", Text = "Test content" };

            _blogPostRepositoryMock.Setup(b => b.GetById(blogpostId)).Returns(blogpost); ;

            _blogPostRepositoryMock.Setup(s => s.Delete(blogpost));

            var request = new DeleteRequest(blogpostId);

            var result = await _handler.Handle(request, CancellationToken.None);

            _blogPostRepositoryMock.Verify(r => r.Delete(blogpost), Times.Once);
            Assert.NotNull(result);

        }

    }
}
