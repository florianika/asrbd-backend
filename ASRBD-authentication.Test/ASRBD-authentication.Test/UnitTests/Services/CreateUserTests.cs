using Application.Ports;
using Application.User.CreateUser.Request;
using Application.User.CreateUser;
using Microsoft.Extensions.Logging;
using Moq;
using Domain;
using Application.User.CreateUser.Response;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class CreateUserTests
    {
        [Fact]
        public async Task Execute_ValidRequest_CreatesUserSuccessfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateUser>>();
            var cryptographyServiceMock = new Mock<ICryptographyService>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            cryptographyServiceMock.Setup(service => service.GenerateSalt()).Returns("MockedSalt");
            cryptographyServiceMock.Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<string>())).Returns("MockedHash");

            var createUser = new CreateUser(loggerMock.Object, cryptographyServiceMock.Object, authRepositoryMock.Object);
            var request = new CreateUserRequest
            {
                Email = "test@example.com",
                Password = "password",
                Name = "John",
                LastName = "Doe",
                Claims = new List<Application.User.CreateUser.Request.Claim> { new Application.User.CreateUser.Request.Claim { Type = "ClaimType", Value = "ClaimValue" } }
            };

            // Act
            var response = await createUser.Execute(request);

            // Assert
            authRepositoryMock.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Once);
            Assert.NotNull(response);

            if (response is CreateUserSuccessResponse successResponse)
            {
                Assert.NotNull(successResponse.UserId);
            }
            else
            {
                // Handle the case when the response is not of type CreateUserSuccessResponse
                Assert.True(false, "Unexpected response type");
            }
        }

    }
}
