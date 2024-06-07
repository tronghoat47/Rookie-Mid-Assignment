using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Application.Services.Impl;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.Unit.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<ICryptographyHelper> _cryptographyHelperMock;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tokenServiceMock = new Mock<ITokenService>();
            _cryptographyHelperMock = new Mock<ICryptographyHelper>();
            _authService = new AuthService(_unitOfWorkMock.Object, _tokenServiceMock.Object, _cryptographyHelperMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserDoesNotExist_ReturnsRegisteredUser()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "test@example.com",
                Name = "Test User",
                Password = "password123",
                RoleId = 1
            };

            User existingUser = null;
            _unitOfWorkMock.Setup(u => u.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(existingUser);

            var salt = "salt";
            var hashedPassword = "hashedPassword";
            _cryptographyHelperMock.Setup(c => c.GenerateSalt()).Returns(salt);
            _cryptographyHelperMock.Setup(c => c.HashPassword(userRequest.Password, salt)).Returns(hashedPassword);

            var expectedUser = new User
            {
                Email = userRequest.Email,
                Name = userRequest.Name,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                RoleId = (byte)userRequest.RoleId,
                Status = StatusUsersConstants.IN_ACTIVE,
                CreatedAt = DateTime.Now,
            };
            _unitOfWorkMock.Setup(u => u.UserRepository.AddAsync(It.IsAny<User>()))
                           .Callback<User>(user => existingUser = user);

            // Act
            var result = await _authService.RegisterAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(expectedUser.PasswordHash, result.PasswordHash);
            Assert.Equal(expectedUser.PasswordSalt, result.PasswordSalt);
            Assert.Equal(expectedUser.RoleId, result.RoleId);
            Assert.Equal(expectedUser.Status, result.Status);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var userRequest = new UserRegisterRequest
            {
                Email = "test@example.com",
                Name = "Test User",
                Password = "password123",
                RoleId = 1
            };

            var existingUser = new User();
            _unitOfWorkMock.Setup(u => u.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(existingUser);

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.RegisterAsync(userRequest));

            // Assert
            Assert.Equal("User already exists", exception.Message);
        }
    }
}