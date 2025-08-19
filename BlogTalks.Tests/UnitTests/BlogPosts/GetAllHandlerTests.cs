using BlogTalks.Application.BlogPosts.Queries;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using Moq;

namespace BlogTalks.Tests.UnitTests.BlogPosts
{
    public class GetAllHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetAllHandler _handler;

        public GetAllHandlerTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new GetAllHandler(_blogPostRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllHandler_Should_ReturnPagedResults_WithCreatorNames()
        {
            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = 1, Title = "Post 1", Text = "Text 1", CreatedBy = 1, Tags = new List<string> { "tag1" } },
                new BlogPost { Id = 2, Title = "Post 2", Text = "Text 2", CreatedBy = 2, Tags = new List<string> { "tag2" } }
            };

            var users = new List<User>
            {
                new User { Id = 1, Username = "Alice" },
                new User { Id = 2, Username = "Bob" }
            };

            _blogPostRepositoryMock.Setup(r =>
                    r.GetAllBlogposts(1, 10, null, null))
                .Returns((2, blogPosts));

            _userRepositoryMock.Setup(r =>
                    r.GetUsersByIds(It.IsAny<List<int>>()))
                .Returns(users);

            var request = new GetAllRequest { PageNumber = 1, PageSize = 10 };
            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(2, result.BlogPosts.Count);
            Assert.Contains(result.BlogPosts, bp => bp.CreatorName == "Alice");
            Assert.Contains(result.BlogPosts, bp => bp.CreatorName == "Bob");
        }


        [Fact]
        public async Task GetAllHandler_ReturnsEmptyList_WhenNoBlogPostsFound()
        {

            var emptyBlogPosts = new List<BlogPost>();
            var emptyUsers = new List<User>();

            _blogPostRepositoryMock.Setup(r =>
                    r.GetAllBlogposts(1, 10, null, null))
                .Returns((0, emptyBlogPosts));

            _userRepositoryMock.Setup(r =>
                    r.GetUsersByIds(It.IsAny<List<int>>()))
                .Returns(emptyUsers);

            var request = new GetAllRequest { PageNumber = 1, PageSize = 10 };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Empty(result.BlogPosts);
            Assert.Equal(0, result.Metadata.TotalCount);
            Assert.Equal(1, result.Metadata.PageNumber);
            Assert.Equal(10, result.Metadata.PageSize);
            Assert.Equal(0, result.Metadata.TotalPages);
        }

    }
}
