

using Application.Ports;
using Application.User.SignOut.Request;
using Application.User.SignOut.Response;
using Application.User.SignOut;
using Microsoft.Extensions.Logging;
using Moq;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class SignOutTests
    {
        [Fact]
        public async Task Execute_ValidRequest_SignsOutUserSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SignOut>>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var signOutService = new SignOut(loggerMock.Object, authRepositoryMock.Object);

            var request = new SignOutRequest
            {
                UserId = Guid.NewGuid() // Provide a valid user ID
            };

            var user = new Domain.User
            {
                Id = request.UserId,
                RefreshToken = new Domain.RefreshToken { Active = true }
            };

            authRepositoryMock.Setup(repo => repo.GetUserByUserId(request.UserId)).ReturnsAsync(user);

            // Act
            var response = await signOutService.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.UpdateRefreshToken(request.UserId, It.IsAny<Domain.RefreshToken>()), Times.Once);
            Assert.IsType<SignOutSuccessResponse>(response);
            Assert.Equal($"User signed out at {DateTime.Now} server time.", (response as SignOutSuccessResponse)?.Message);
        }


        [Fact]
        public async Task Execute_UserNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SignOut>>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var signOutService = new SignOut(loggerMock.Object, authRepositoryMock.Object);

            var request = new SignOutRequest
            {
                UserId = Guid.NewGuid() // Provide a valid user ID
            };

            authRepositoryMock.Setup(repo => repo.GetUserByUserId(request.UserId)).ReturnsAsync((Domain.User)null);

            // Act
            var response = await signOutService.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.UpdateRefreshToken(It.IsAny<Guid>(), It.IsAny<Domain.RefreshToken>()), Times.Never);
            Assert.IsType<SignOutErrorResponse>(response);
            Assert.Equal("User not found", (response as SignOutErrorResponse)?.Message);
        }

    }
}
