using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Moq;

namespace BlogTalks.Tests.UnitTests.BlogPosts
{
    public class GetByIdHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogpostRepoMock;
        private readonly GetByIdHandler _handler;
        public GetByIdHandlerTests()
        {
            _blogpostRepoMock = new Mock<IBlogPostRepository>();
            _handler = new GetByIdHandler(_blogpostRepoMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnException_WhenBlogpostDoestNotExist()
        {
            var blogpostId = 123;
            var query = new GetByIdRequest (blogpostId);

            await Assert.ThrowsAsync<BlogTalksException>(() => _handler.Handle(query, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Should_ReturnBlogpostResponse_WhenBlogpostExist()
        {
            var blogpostId = 1;
            var blogPost = new BlogPost
            {
                Id = blogpostId,
                Title = "Title",
                Text = "Text",
                Tags = new List<string> { "Tag1" },
                CreatedAt = DateTime.Now,
                CreatedBy = 5
            };

            _blogpostRepoMock
                .Setup(repo => repo.GetById(blogpostId))
                .Returns(blogPost);

            var query = new GetByIdRequest (blogpostId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(blogpostId,result.Id);
            Assert.Equal(blogPost.Title,result.Title);
        }
    }
}
