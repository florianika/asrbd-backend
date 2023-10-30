
using Application.Ports;
using Application.User;
using Application.User.CreateUser;
using Application.User.CreateUser.Response;
using Application.User.RefreshToken;
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class AuthTokenServiceTests
    {
        [Fact]
        public async Task GenerateRefreshToken_ReturnsNonNullOrEmptyString()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RefreshToken>>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var refreshTokenSrv = new RefreshToken(loggerMock.Object, authTokenServiceMock.Object, authRepositoryMock.Object);

            var request = new RefreshTokenRequest
            {
                AccessToken = "Access Token",
                RefreshToken = "Refresh Token"
            };
            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                RefreshToken = new Domain.RefreshToken
                {
                    Value = "Refresh Token",
                    Active = true,
                    ExpirationDate = DateTime.Now.AddMinutes(10) // Set an expiration date in the past (expired)
                }
            };

            authTokenServiceMock.Setup(service => service.IsTokenValid(It.IsAny<string>(), false)).ReturnsAsync(true);
            authTokenServiceMock.Setup(service => service.GetUserIdFromToken(It.IsAny<string>())).ReturnsAsync(sampleUser.Id);
            authRepositoryMock.Setup(repo => repo.GetUserByUserId(It.IsAny<Guid>())).ReturnsAsync(sampleUser);

            authTokenServiceMock.Setup(service => service.GenerateRefreshToken())
                              .ReturnsAsync("sampleRefreshToken");
            authTokenServiceMock.Setup(service => service.GenerateAccessToken(It.IsAny<Domain.User>()))
                               .ReturnsAsync("validAccessToken");
            // Act
            var response = await refreshTokenSrv.Execute(request);

            // Assert
            Assert.NotNull(response);
            if (response is RefreshTokenSuccessResponse successResponse)
            {
                Assert.NotNull(successResponse.RefreshToken);
                Assert.NotEmpty(successResponse.RefreshToken);
            }
            else
            {
                // Handle the case when the response is not of type CreateUserSuccessResponse
                Assert.True(false, "Unexpected response type");
            }
        }
    }
}
