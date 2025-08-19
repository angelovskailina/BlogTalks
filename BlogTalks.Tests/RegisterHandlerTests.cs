//using BlogTalks.Application.Users.Commands;
//using BlogTalks.Domain.Entities;
//using BlogTalks.Domain.Repositories;
//using Moq;

//namespace BlogTalks.Tests
//{
//    public class RegisterHandlerTests
//    {
//        private readonly Mock<IUserRepository> _userRepositoryMock;

//        public RegisterHandlerTests()
//        {
//            _userRepositoryMock = new Mock<IUserRepository>();
//        }

//        [Fact]
//        public async Task Handle_Should_ReturnFailureMessage_WhenUsernameIsNotUnique()
//        {
//            var command = new RegisterRequest("test123", "test", "test12345", "test@mail.com");
//            _userRepositoryMock
//                .Setup(u => u.GetUsername(command.Username))
//                .Returns(new User { Username = command.Username });
//        }
//    }
//}
