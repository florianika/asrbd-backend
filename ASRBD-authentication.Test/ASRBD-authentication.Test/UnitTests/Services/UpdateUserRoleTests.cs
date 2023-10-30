
using Application.Ports;
using Application.User.UpdateUserRole.Request;
using Application.User.UpdateUserRole;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using Moq;
using Application.Exceptions;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class UpdateUserRoleTests
    {
        [Fact]
        public async Task Execute_ValidRequest_UpdatesUserRoleSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateUserRole>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(true);

            var updateUserRole = new UpdateUserRole(loggerMock.Object, authRepositoryMock.Object);
            var request = new UpdateUserRoleRequest
            {
                UserId = Guid.NewGuid(),
                AccountRole = AccountRole.USER
            };

            // Act
            await updateUserRole.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateUserRole(request.UserId, request.AccountRole), Times.Once);
        }
        [Fact]
        public async Task Execute_UserNotFound_ThrowsException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateUserRole>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            authRepositoryMock.Setup(repo => repo.CheckIfUserExists(It.IsAny<Guid>())).ReturnsAsync(false);

            var updateUserRole = new UpdateUserRole(loggerMock.Object, authRepositoryMock.Object);
            var request = new UpdateUserRoleRequest
            {
                UserId = Guid.NewGuid(),
                AccountRole = AccountRole.USER
            };

            // Act & Assert
            await Assert.ThrowsAsync<DirectoryNotFoundException>(() => updateUserRole.Execute(request));
            authRepositoryMock.Verify(repo => repo.CheckIfUserExists(request.UserId), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateUserRole(It.IsAny<Guid>(), It.IsAny<AccountRole>()), Times.Never);
        }       
    }
}
