using Application.Ports;
using Application.User.GetAllUsers.Response;
using Application.User.GetAllUsers;
using Domain.Enum;
using Microsoft.Extensions.Logging;
using Moq;


namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class GetAllUsersTests
    {
        [Fact]
        public async Task Execute_ReturnsAllUsersSuccessfully()
        {
            // Arrange
            var authRepositoryMock = new Mock<IAuthRepository>();
            var loggerMock = new Mock<ILogger<GetAllUsers>>();

            var getAllUsersService = new GetAllUsers(authRepositoryMock.Object, loggerMock.Object);

            var users = new List<Domain.User>
                {
                    new Domain.User { Id = Guid.NewGuid(), Name = "User1", Email = "user1@example.com" },
                    new Domain.User { Id = Guid.NewGuid(), Name = "User2", Email = "user2@example.com" }
                    // Add more user instances as needed
                };

            authRepositoryMock.Setup(repo => repo.GetAllUsers(Guid.Empty)).ReturnsAsync(users);

            // Act
            var response = await getAllUsersService.Execute(Guid.Empty);

            // Assert
            authRepositoryMock.Verify(repo => repo.GetAllUsers(Guid.Empty), Times.Once);
            Assert.IsType<GetAllUsersSuccessResponse>(response);
            Assert.NotNull(response);

            if (response is GetAllUsersSuccessResponse successResponse)
            {
                Assert.NotNull(successResponse.UsersDTO);
                Assert.Equal(users.Count, successResponse.UsersDTO.Count());
            }
            else
            {
                Assert.Fail("Unexpected response type");
            }
        }
    }
}

