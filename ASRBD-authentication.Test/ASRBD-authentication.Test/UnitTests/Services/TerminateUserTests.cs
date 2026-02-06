
using Application.Exceptions;
using Application.Ports;
using Application.User.TerminateUser.Request;
using Application.User.TerminateUser;
using Microsoft.Extensions.Logging;
using Moq;
using Domain.Enum;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class TerminateUserTests
    {
        [Fact]
        public async Task Execute_ValidRequest_TerminatesUserSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TerminateUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(true);

            var terminateUser = new TerminateUser(loggerMock.Object, authRepositoryMock.Object);
            var request = new TerminateUserRequest
            {
                UserId = Guid.NewGuid(),
                RequestUserId = Guid.NewGuid(),
                RequestUserRole = "ADMIN"
            };

            // Act
            await terminateUser.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateAccountUser(request.UserId, AccountStatus.TERMINATED, request.RequestUserRole), Times.Once);
        }

        [Fact]
        public async Task Execute_UserNotFound_ThrowsException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TerminateUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(false);

            var terminateUser = new TerminateUser(loggerMock.Object, authRepositoryMock.Object);
            var request = new TerminateUserRequest
            {
                UserId = Guid.NewGuid()
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => terminateUser.Execute(request));
            Assert.Equal("User not found", exception.Message);
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateAccountUser(It.IsAny<Guid>(), It.IsAny<AccountStatus>(), It.IsAny<string>()), Times.Never);
        }
    }

}
