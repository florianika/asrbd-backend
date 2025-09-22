﻿using Application.Configuration;
using Application.User.CreateUser.Request;
using Application.User.CreateUser.Response;
using Application.User.CreateUser;
using Moq;
using WebApi.Controllers;
using Application.User.ActivateUser;
using Application.User.GetAllUsers;
using Application.User.GetUser;
using Application.User.Login;
using Application.User.SignOut;
using Application.User.TerminateUser;
using Application.User.UpdateUserRole;
using Application.User.RefreshToken;
using Application.Ports;
using Microsoft.Extensions.Logging;
using Application.User.Login.Request;
using Application.User.Login.Response;
using Domain.Enum;
using Application.Exceptions;
using Application.User.GetUserByEmail;
using Application.User.RefreshToken.Request;
using Application.User.RefreshToken.Response;
using Application.User.SetUserMunicipality;
using Microsoft.Extensions.Options;
using Application.Queries;
using Infrastructure.Queries.GetMunicipalities;
using Application.User.Login2fa;
using Application.User.Verify2fa;

namespace ASRBD_authentication.Test.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task CreateUser_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var createUserServiceMock = new Mock<CreateUser>(
                Mock.Of<ILogger<CreateUser>>(),
                Mock.Of<ICryptographyService>(),
                Mock.Of<IAuthRepository>()
            );
            createUserServiceMock
                .Setup(service => service.Execute(It.IsAny<CreateUserRequest>()))
                .ReturnsAsync(new CreateUserSuccessResponse { UserId = Guid.NewGuid() });

            var loginServiceMock = new Mock<Login>(
                Mock.Of<ILogger<Login>>(),
                Mock.Of<IAuthRepository>(),
                Mock.Of<ICryptographyService>(),
                Mock.Of<IAuthTokenService>()
                );

            var login2faServiceMock = new Mock<Login2fa>(
           Mock.Of<ILogger<Login2fa>>(),
           Mock.Of<IAuthRepository>(),
           Mock.Of<ICryptographyService>(),
           Mock.Of<IAuthTokenService>(),
           Mock.Of<IOtpRepository>()
           );
            var verify2faServiceMock = new Mock<Verify2fa>(
           Mock.Of<ILogger<Verify2fa>>(),
           Mock.Of<IAuthRepository>(),
           Mock.Of<ICryptographyService>(),
           Mock.Of<IAuthTokenService>(),
           Mock.Of<IOtpRepository>()
           );
            var refreshTokenServiceMock = new Mock<RefreshToken>(
                Mock.Of<ILogger<RefreshToken>>(),
                Mock.Of<IAuthTokenService>(),
                Mock.Of<IAuthRepository>()
                );

            var signOutServiceMock = new Mock<SignOut>(
                Mock.Of<ILogger<SignOut>>(),
                Mock.Of<IAuthRepository>()
                );

            var getAllUsersServiceMock = new Mock<GetAllUsers>(
                Mock.Of<IAuthRepository>(),
                Mock.Of<ILogger<GetAllUsers>>()
                );

            var updateUserRoleServiceMock = new Mock<UpdateUserRole>(
                Mock.Of<ILogger<UpdateUserRole>>(),
                Mock.Of<IAuthRepository>()
                );

            var terminateUserServiceMock = new Mock<TerminateUser>(
                Mock.Of<ILogger<TerminateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var activateUserServiceMock = new Mock<ActivateUser>(
                Mock.Of<ILogger<ActivateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var getUserServiceMock = new Mock<GetUser>(
                Mock.Of<ILogger<GetUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var getUserByEmailServiceMock = new Mock<GetUserByEmail>(
                Mock.Of<ILogger<GetUserByEmail>>(),
                Mock.Of<IAuthRepository>()
            );

            var setUserMunicipalityServiceMock = new Mock<SetUserMunicipality>(
                Mock.Of<ILogger<SetUserMunicipality>>(),
                Mock.Of<IAuthRepository>()
            );
            var getMunicipalitiesQuery = new Mock<IGetMunicipalitiesQuery>();

            var gisServerCredentials = new Mock<IOptions<GisServerCredentials>>();
            var gisFormRequest = new Mock<IOptions<GisFormRequest>>();
            var iHttpClientFactoryMock = new Mock<IHttpClientFactory>();

            var controller = new AuthController(
                createUserServiceMock.Object,
                loginServiceMock.Object,
                refreshTokenServiceMock.Object,
                signOutServiceMock.Object,
                getAllUsersServiceMock.Object,
                updateUserRoleServiceMock.Object,
                terminateUserServiceMock.Object,
                activateUserServiceMock.Object,
                getUserServiceMock.Object,
                getUserByEmailServiceMock.Object,
                gisServerCredentials.Object,
                gisFormRequest.Object,
                setUserMunicipalityServiceMock.Object,
                getMunicipalitiesQuery.Object,
                iHttpClientFactoryMock.Object,
                login2faServiceMock.Object,
                verify2faServiceMock.Object
            );

            // Act
            var result = await controller.CreateUser(new CreateUserRequest());

            // Assert
            var createdResult = Assert.IsType<CreateUserSuccessResponse>(result);
            Assert.NotEqual(Guid.Empty, createdResult.UserId); // Replace NotNull check with NotEqual to Guid.Empty
        }

        [Fact]
        public async Task Login_ValidRequest_ReturnsLoginSuccessResponse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<Login>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var cryptographyServiceMock = new Mock<ICryptographyService>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var logger2faMock = new Mock<ILogger<Login2fa>>();
            var verify2faMock = new Mock<ILogger<Verify2fa>>();
            var otpRepositoryMock = new Mock<IOtpRepository>();
            // Set up a sample user
            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                Email = "aa@aa.aa",
                Password = "password", // Set the actual hashed password
                Salt = "sampleSalt",
                AccountStatus = AccountStatus.ACTIVE, // You might want to set other properties as needed
                RefreshToken = new Domain.RefreshToken
                {
                    UserId = Guid.NewGuid(),
                    Value = "sampleRefreshToken",
                    Active = true,
                    ExpirationDate = DateTime.Now.AddDays(1)
                }
            };

            authRepositoryMock.Setup(repo => repo.FindUserByEmail(It.IsAny<string>()))
                              .ReturnsAsync(sampleUser);

            cryptographyServiceMock.Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<string>()))
                                  .Returns("password"); // Replace with the actual hashed password

            authTokenServiceMock.Setup(service => service.GenerateRefreshToken())
                               .ReturnsAsync("sampleRefreshToken");

            authTokenServiceMock.Setup(service => service.GetRefreshTokenLifetimeInMinutes())
                               .ReturnsAsync(60); // Set the actual refresh token lifetime

            authTokenServiceMock.Setup(service => service.GenerateIdToken(It.IsAny<Domain.User>()))
                    .ReturnsAsync("validIdToken");

            authTokenServiceMock.Setup(service => service.GenerateAccessToken(It.IsAny<Domain.User>()))
                                .ReturnsAsync("validAccessToken");

            // Arrange
            var createUserServiceMock = new Mock<CreateUser>(
                Mock.Of<ILogger<CreateUser>>(),
                Mock.Of<ICryptographyService>(),
                Mock.Of<IAuthRepository>()
            );
            var loginService = new Login(loggerMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object);

            var login2faService = new Login2fa(logger2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object,otpRepositoryMock.Object);

            var verify2faService = new Verify2fa(verify2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object, otpRepositoryMock.Object);

            var refreshTokenServiceMock = new Mock<RefreshToken>(
                Mock.Of<ILogger<RefreshToken>>(),
                Mock.Of<IAuthTokenService>(),
                Mock.Of<IAuthRepository>()
                );

            var signOutServiceMock = new Mock<SignOut>(
                Mock.Of<ILogger<SignOut>>(),
                Mock.Of<IAuthRepository>()
                );

            var getAllUsersServiceMock = new Mock<GetAllUsers>(
                Mock.Of<IAuthRepository>(),
                Mock.Of<ILogger<GetAllUsers>>()
                );

            var updateUserRoleServiceMock = new Mock<UpdateUserRole>(
                Mock.Of<ILogger<UpdateUserRole>>(),
                Mock.Of<IAuthRepository>()
                );

            var terminateUserServiceMock = new Mock<TerminateUser>(
                Mock.Of<ILogger<TerminateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var activateUserServiceMock = new Mock<ActivateUser>(
                Mock.Of<ILogger<ActivateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var getUserServiceMock = new Mock<GetUser>(
                Mock.Of<ILogger<GetUser>>(),
                Mock.Of<IAuthRepository>()
                );
            
            var getUserByEmailServiceMock = new Mock<GetUserByEmail>(
                Mock.Of<ILogger<GetUserByEmail>>(),
                Mock.Of<IAuthRepository>()
            );
            
            var setUserMunicipalityServiceMock = new Mock<SetUserMunicipality>(
                Mock.Of<ILogger<SetUserMunicipality>>(),
                Mock.Of<IAuthRepository>()
            );
            var getMunicipalitiesQuery = new Mock<IGetMunicipalitiesQuery>();

            var gisServerCredentials = new Mock<IOptions<GisServerCredentials>>();
            var gisFormRequest = new Mock<IOptions<GisFormRequest>>();
            var iHttpClientFactoryMock = new Mock<IHttpClientFactory>();
            
            var controller = new AuthController(
                createUserServiceMock.Object,
                loginService,
                refreshTokenServiceMock.Object,
                signOutServiceMock.Object,
                getAllUsersServiceMock.Object,
                updateUserRoleServiceMock.Object,
                terminateUserServiceMock.Object,
                activateUserServiceMock.Object,
                getUserServiceMock.Object,
                getUserByEmailServiceMock.Object,
                gisServerCredentials.Object,
                gisFormRequest.Object,
                setUserMunicipalityServiceMock.Object,
                getMunicipalitiesQuery.Object,
                iHttpClientFactoryMock.Object,
                login2faService,
                verify2faService
            );

            // Act
            var result = await controller.Login(new LoginRequest { Email = "aa@aa.aa", Password = "string" });

            // Assert
            var successResponse = Assert.IsType<LoginSuccessResponse>(result);
            Assert.NotNull(successResponse.IdToken);
            Assert.NotNull(successResponse.AccessToken);
            Assert.Equal("sampleRefreshToken", successResponse.RefreshToken);

            authRepositoryMock.Verify(repo => repo.UnlockAccount(It.IsAny<Domain.User>()), Times.Once);
            authRepositoryMock.Verify(repo => repo.UpdateRefreshToken(It.IsAny<Guid>(), It.IsAny<Domain.RefreshToken>()), Times.Once);
        }

        [Fact]
        public async Task Login_InvalidPassword_ReturnsForbiddenException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<Login>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var cryptographyServiceMock = new Mock<ICryptographyService>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var OtpRepositoryMock = new Mock<IOtpRepository>();
            var logger2faMock = new Mock<ILogger<Login2fa>>();
            var verifyLogger2faMock = new Mock<ILogger<Verify2fa>>();
            // Set up a sample user with an incorrect password
            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "correctHashedPassword", // Set the actual correct hashed password
                Salt = "sampleSalt",
                AccountStatus = AccountStatus.ACTIVE,
                RefreshToken = new Domain.RefreshToken
                {
                    UserId = Guid.NewGuid(),
                    Value = "sampleRefreshToken",
                    Active = true,
                    ExpirationDate = DateTime.Now.AddDays(1)
                }
            };

            authRepositoryMock.Setup(repo => repo.FindUserByEmail(It.IsAny<string>()))
                              .ReturnsAsync(sampleUser);

            cryptographyServiceMock.Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<string>()))
                                  .Returns("incorrectHashedPassword"); // Set an incorrect hashed password

            var createUserServiceMock = new Mock<CreateUser>(
               Mock.Of<ILogger<CreateUser>>(),
               Mock.Of<ICryptographyService>(),
               Mock.Of<IAuthRepository>()
           );

            var loginService = new Login(loggerMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object);
            var login2faService = new Login2fa(logger2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object, OtpRepositoryMock.Object);
            var verify2faService = new Verify2fa(verifyLogger2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object, OtpRepositoryMock.Object);

            var refreshTokenServiceMock = new Mock<RefreshToken>(
                Mock.Of<ILogger<RefreshToken>>(),
                Mock.Of<IAuthTokenService>(),
                Mock.Of<IAuthRepository>()
                );

            var signOutServiceMock = new Mock<SignOut>(
                Mock.Of<ILogger<SignOut>>(),
                Mock.Of<IAuthRepository>()
                );

            var getAllUsersServiceMock = new Mock<GetAllUsers>(
                Mock.Of<IAuthRepository>(),
                Mock.Of<ILogger<GetAllUsers>>()
                );

            var updateUserRoleServiceMock = new Mock<UpdateUserRole>(
                Mock.Of<ILogger<UpdateUserRole>>(),
                Mock.Of<IAuthRepository>()
                );

            var terminateUserServiceMock = new Mock<TerminateUser>(
                Mock.Of<ILogger<TerminateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var activateUserServiceMock = new Mock<ActivateUser>(
                Mock.Of<ILogger<ActivateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var getUserServiceMock = new Mock<GetUser>(
                Mock.Of<ILogger<GetUser>>(),
                Mock.Of<IAuthRepository>()
                );
            
            var getUserByEmailServiceMock = new Mock<GetUserByEmail>(
                Mock.Of<ILogger<GetUserByEmail>>(),
                Mock.Of<IAuthRepository>()
            );
            
            var setUserMunicipalityServiceMock = new Mock<SetUserMunicipality>(
                Mock.Of<ILogger<SetUserMunicipality>>(),
                Mock.Of<IAuthRepository>()
            );
            var getMunicipalitiesQuery = new Mock<IGetMunicipalitiesQuery>();
            var gisServerCredentials = new Mock<IOptions<GisServerCredentials>>();
            var gisFormRequest = new Mock<IOptions<GisFormRequest>>();
            var iHttpClientFactoryMock = new Mock<IHttpClientFactory>();
            
            var controller = new AuthController(
                createUserServiceMock.Object,
                loginService,
                refreshTokenServiceMock.Object,
                signOutServiceMock.Object,
                getAllUsersServiceMock.Object,
                updateUserRoleServiceMock.Object,
                terminateUserServiceMock.Object,
                activateUserServiceMock.Object,
                getUserServiceMock.Object,
                getUserByEmailServiceMock.Object,
                gisServerCredentials.Object,
                gisFormRequest.Object,
                setUserMunicipalityServiceMock.Object,
                getMunicipalitiesQuery.Object,
                iHttpClientFactoryMock.Object,
                login2faService, 
                verify2faService
            );
            authTokenServiceMock.Setup(service => service.GenerateIdToken(It.IsAny<Domain.User>()))
                    .ReturnsAsync("validIdToken");

            authTokenServiceMock.Setup(service => service.GenerateAccessToken(It.IsAny<Domain.User>()))
                                .ReturnsAsync("validAccessToken");
            // Act and Assert
            await Assert.ThrowsAsync<ForbidenException>(() => controller.Login(new LoginRequest { Email = "test@example.com", Password = "incorrectPassword" }));
        }

        [Fact]
        public async Task Login_LockedAccount_ReturnsForbiddenException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<Login>>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var cryptographyServiceMock = new Mock<ICryptographyService>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var logger2faMock = new Mock<ILogger<Login2fa>>();
            var verifyLogger2faMock = new Mock<ILogger<Verify2fa>>();
            var otpRepositoryMock = new Mock<IOtpRepository>();

            // Set up a sample user with a locked account
            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "correctHashedPassword", // Set the actual correct hashed password
                Salt = "sampleSalt",
                AccountStatus = AccountStatus.LOCKED,
                LockExpiration = DateTime.Now.AddDays(1) // Set an expiration date in the future
            };

            authRepositoryMock.Setup(repo => repo.FindUserByEmail(It.IsAny<string>()))
                              .ReturnsAsync(sampleUser);

            cryptographyServiceMock.Setup(service => service.HashPassword(It.IsAny<string>(), It.IsAny<string>()))
                                  .Returns("correctHashedPassword"); // Set the actual correct hashed password

            var createUserServiceMock = new Mock<CreateUser>(
               Mock.Of<ILogger<CreateUser>>(),
               Mock.Of<ICryptographyService>(),
               Mock.Of<IAuthRepository>()
           );

            var loginService = new Login(loggerMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object);

            var login2faService = new Login2fa(logger2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object, otpRepositoryMock.Object);
            var verify2faService = new Verify2fa(verifyLogger2faMock.Object, authRepositoryMock.Object, cryptographyServiceMock.Object, authTokenServiceMock.Object, otpRepositoryMock.Object);

            var refreshTokenServiceMock = new Mock<RefreshToken>(
                Mock.Of<ILogger<RefreshToken>>(),
                Mock.Of<IAuthTokenService>(),
                Mock.Of<IAuthRepository>()
                );

            var signOutServiceMock = new Mock<SignOut>(
                Mock.Of<ILogger<SignOut>>(),
                Mock.Of<IAuthRepository>()
                );

            var getAllUsersServiceMock = new Mock<GetAllUsers>(
                Mock.Of<IAuthRepository>(),
                Mock.Of<ILogger<GetAllUsers>>()
                );

            var updateUserRoleServiceMock = new Mock<UpdateUserRole>(
                Mock.Of<ILogger<UpdateUserRole>>(),
                Mock.Of<IAuthRepository>()
                );

            var terminateUserServiceMock = new Mock<TerminateUser>(
                Mock.Of<ILogger<TerminateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var activateUserServiceMock = new Mock<ActivateUser>(
                Mock.Of<ILogger<ActivateUser>>(),
                Mock.Of<IAuthRepository>()
                );

            var getUserServiceMock = new Mock<GetUser>(
                Mock.Of<ILogger<GetUser>>(),
                Mock.Of<IAuthRepository>()
                );
            
            var getUserByEmailServiceMock = new Mock<GetUserByEmail>(
                Mock.Of<ILogger<GetUserByEmail>>(),
                Mock.Of<IAuthRepository>()
            );
            
            var setUserMunicipalityServiceMock = new Mock<SetUserMunicipality>(
                Mock.Of<ILogger<SetUserMunicipality>>(),
                Mock.Of<IAuthRepository>()
            );
            var getMunicipalitiesQuery = new Mock<IGetMunicipalitiesQuery>();

            var gisServerCredentials = new Mock<IOptions<GisServerCredentials>>();
            var gisFormRequest = new Mock<IOptions<GisFormRequest>>();
            var iHttpClientFactoryMock = new Mock<IHttpClientFactory>();
            
            var controller = new AuthController(
                createUserServiceMock.Object,
                loginService,
                refreshTokenServiceMock.Object,
                signOutServiceMock.Object,
                getAllUsersServiceMock.Object,
                updateUserRoleServiceMock.Object,
                terminateUserServiceMock.Object,
                activateUserServiceMock.Object,
                getUserServiceMock.Object,
                getUserByEmailServiceMock.Object,
                gisServerCredentials.Object,
                gisFormRequest.Object,
                setUserMunicipalityServiceMock.Object,
                getMunicipalitiesQuery.Object,
                iHttpClientFactoryMock.Object,
                login2faService,
                verify2faService
            );

            // Act and Assert
            await Assert.ThrowsAsync<ForbidenException>(() => controller.Login(new LoginRequest { Email = "test@example.com", Password = "correctPassword" }));
        }

        [Fact]
        public async Task RefreshToken_ValidRequest_ReturnsRefreshTokenSuccessResponse()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RefreshToken>>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var refreshTokenService = new RefreshToken(loggerMock.Object, authTokenServiceMock.Object, authRepositoryMock.Object);

            var request = new RefreshTokenRequest
            {
                AccessToken = "validAccessToken",
                RefreshToken = "validRefreshToken"
            };

            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                RefreshToken = new Domain.RefreshToken
                {
                    Value = "validRefreshToken",
                    Active = true,
                    ExpirationDate = DateTime.Now.AddMinutes(10) // Set an expiration date in the future
                }
            };

            authTokenServiceMock.Setup(service => service.IsTokenValid(It.IsAny<string>(), false)).ReturnsAsync(true);
            authTokenServiceMock.Setup(service => service.GetUserIdFromToken(It.IsAny<string>())).ReturnsAsync(sampleUser.Id);
            authRepositoryMock.Setup(repo => repo.GetUserByUserId(It.IsAny<Guid>())).ReturnsAsync(sampleUser);
            authTokenServiceMock.Setup(service => service.GenerateAccessToken(It.IsAny<Domain.User>()))
                              .ReturnsAsync("newAccessToken"); // Set the actual new access token value
            authTokenServiceMock.Setup(service => service.GenerateRefreshToken()).ReturnsAsync("newRefreshToken"); // Set the actual new refresh token value
            authTokenServiceMock.Setup(service => service.GetRefreshTokenLifetimeInMinutes()).ReturnsAsync(20); // Set the actual refresh token lifetime

            // Act
            var response = await refreshTokenService.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<RefreshTokenSuccessResponse>(response);
            var successResponse = (RefreshTokenSuccessResponse)response;
            Assert.Equal("newAccessToken", successResponse.AccessToken); // Verify against the actual new access token value
            Assert.Equal("newRefreshToken", successResponse.RefreshToken); // Verify against the actual new refresh token value
            Assert.True(successResponse.RefreshTokenExpirationDate > DateTime.MinValue); // Replace NotNull check with a valid comparison
        }

        [Fact]
        public async Task RefreshToken_InvalidAccessToken_ThrowsInvalidTokenException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RefreshToken>>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var refreshTokenService = new RefreshToken(loggerMock.Object, authTokenServiceMock.Object, authRepositoryMock.Object);

            var request = new RefreshTokenRequest
            {
                AccessToken = "invalidAccessToken",
                RefreshToken = "validRefreshToken"
            };

            authTokenServiceMock.Setup(service => service.IsTokenValid(It.IsAny<string>(), false)).ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidTokenException>(() => refreshTokenService.Execute(request));
        }

        [Fact]
        public async Task RefreshToken_ExpiredRefreshToken_ThrowsInvalidTokenException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<RefreshToken>>();
            var authTokenServiceMock = new Mock<IAuthTokenService>();
            var authRepositoryMock = new Mock<IAuthRepository>();

            var refreshTokenService = new RefreshToken(loggerMock.Object, authTokenServiceMock.Object, authRepositoryMock.Object);

            var request = new RefreshTokenRequest
            {
                AccessToken = "validAccessToken",
                RefreshToken = "expiredRefreshToken"
            };

            var sampleUser = new Domain.User
            {
                Id = Guid.NewGuid(),
                RefreshToken = new Domain.RefreshToken
                {
                    Value = "expiredRefreshToken",
                    Active = true,
                    ExpirationDate = DateTime.Now.AddMinutes(-10) // Set an expiration date in the past (expired)
                }
            };

            authTokenServiceMock.Setup(service => service.IsTokenValid(It.IsAny<string>(), false)).ReturnsAsync(true);
            authTokenServiceMock.Setup(service => service.GetUserIdFromToken(It.IsAny<string>())).ReturnsAsync(sampleUser.Id);
            authRepositoryMock.Setup(repo => repo.GetUserByUserId(It.IsAny<Guid>())).ReturnsAsync(sampleUser);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidTokenException>(() => refreshTokenService.Execute(request));
        }

    }
}
