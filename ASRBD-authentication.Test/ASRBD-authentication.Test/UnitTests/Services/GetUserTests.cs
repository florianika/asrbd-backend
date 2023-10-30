using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Ports;
using Application.User.GetUser.Request;
using Application.User.GetUser.Response;
using Application.User.GetUser;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ASRBD_authentication.Test.UnitTests.Services
{
    public class GetUserTests
    {
        [Fact]
        public async Task Execute_UserFound_ReturnsUserDTO()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GetUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>(); // Replace with actual repository setup
            var userService = new GetUser(loggerMock.Object, authRepositoryMock.Object);

            var userId = Guid.NewGuid();
            var user = new Domain.User // Replace with actual user setup
            {
                Id = userId,
                // Other properties...
            };

            authRepositoryMock.Setup(repo => repo.FindUserById(userId))
                .ReturnsAsync(user);

            // Act
            var result = await userService.Execute(new GetUserRequest { UserId = userId });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetUserSuccessResponse>(result);
            var successResponse = result as GetUserSuccessResponse;
            Assert.NotNull(successResponse?.UserDTO);
            // Add additional assertions based on your DTO structure
        }

        [Fact]
        public async Task Execute_UserNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GetUser>>();
            var authRepositoryMock = new Mock<IAuthRepository>(); // Replace with actual repository setup
            var userService = new GetUser(loggerMock.Object, authRepositoryMock.Object);

            var userId = Guid.NewGuid();

            authRepositoryMock.Setup(repo => repo.FindUserById(userId))
                .ReturnsAsync((Domain.User)null); // User not found

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => userService.Execute(new GetUserRequest { UserId = userId }));
        }
    }
}


