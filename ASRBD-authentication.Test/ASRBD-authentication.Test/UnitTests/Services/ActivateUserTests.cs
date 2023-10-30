using Application.Exceptions;
using Application.Ports;
using Application.User.ActivateUser.Request;
using Application.User.ActivateUser;
using Microsoft.Extensions.Logging;
using Moq;
using Domain.Enum;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    // Assuming you're using xUnit for testing
    public class ActivateUserTests
    {
        [Fact]
        public async Task Execute_ValidRequest_ActivatesUserSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ActivateUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(true);

            var activateUser = new ActivateUser(loggerMock.Object, authRepositoryMock.Object);
            var request = new ActivateUserRequest
            {
                UserId = Guid.NewGuid()
            };

            // Act
            await activateUser.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateAccountUser(request.UserId, AccountStatus.ACTIVE), Times.Once);
        }

        [Fact]
        public async Task Execute_UserNotFound_ThrowsException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ActivateUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(false);

            var activateUser = new ActivateUser(loggerMock.Object, authRepositoryMock.Object);
            var request = new ActivateUserRequest
            {
                UserId = Guid.NewGuid()
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => activateUser.Execute(request));
            Assert.Equal("User not found", exception.Message);
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateAccountUser(It.IsAny<Guid>(), It.IsAny<AccountStatus>()), Times.Never);
        }
    }

}
