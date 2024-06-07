using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using BaseProject.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.Unit.Controller
{
    public class LovedBookControllerTest
    {
        private readonly Mock<ILovedBookService> _lovedBookServiceMock;
        private readonly LovedBookController _lovedBookController;

        public LovedBookControllerTest()
        {
            _lovedBookServiceMock = new Mock<ILovedBookService>();
            _lovedBookController = new LovedBookController(_lovedBookServiceMock.Object);
        }

        [Fact]
        public async Task GetLovedBooksByUser_WhenLovedBooksExist_ReturnsOk()
        {
            // Arrange
            string userId = "user123";
            var lovedBooks = new List<LovedBookResponse>
            {
                new LovedBookResponse { BookId = 1, BookName = "Book 1" },
                new LovedBookResponse { BookId = 2, BookName = "Book 2" }
            };

            _lovedBookServiceMock.Setup(x => x.GetLovedBooksByUser(userId)).ReturnsAsync(lovedBooks);

            // Act
            var result = await _lovedBookController.GetLovedBooksByUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal("Get loved book successfully", response.Message);
            Assert.Equal(lovedBooks.ToList(), response.Data);
        }

        [Fact]
        public async Task GetLovedBooksByUser_WhenNoLovedBooksFound_ReturnsNotFound()
        {
            // Arrange
            string userId = "user123";

            _lovedBookServiceMock.Setup(x => x.GetLovedBooksByUser(userId)).ReturnsAsync((List<LovedBookResponse>)null);

            // Act
            var result = await _lovedBookController.GetLovedBooksByUser(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(notFoundResult.Value);

            Assert.False(response.Success);
            Assert.Equal("Loved book not found", response.Message);
        }

        [Fact]
        public async Task GetLovedBooksByUser_WhenExceptionThrown_ReturnsConflict()
        {
            // Arrange
            string userId = "user123";

            _lovedBookServiceMock.Setup(x => x.GetLovedBooksByUser(userId)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _lovedBookController.GetLovedBooksByUser(userId);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);

            Assert.False(response.Success);
            Assert.Equal("An error occurred", response.Message);
        }

        [Fact]
        public async Task CreateLovedBook_WhenCreateSucceeds_ReturnsOk()
        {
            // Arrange
            var request = new LovedBookRequest();
            _lovedBookServiceMock.Setup(x => x.CreateLovedBook(request)).ReturnsAsync(true);

            // Act
            var result = await _lovedBookController.CreateLovedBook(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal("Create loved book successfully", response.Message);
        }

        [Fact]
        public async Task CreateLovedBook_WhenCreateFails_ReturnsConflict()
        {
            // Arrange
            var request = new LovedBookRequest();
            _lovedBookServiceMock.Setup(x => x.CreateLovedBook(request)).ReturnsAsync(false);

            // Act
            var result = await _lovedBookController.CreateLovedBook(request);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);

            Assert.False(response.Success);
            Assert.Equal("Create loved book failed", response.Message);
        }

        [Fact]
        public async Task CreateLovedBook_WhenExceptionThrown_ReturnsConflict()
        {
            // Arrange
            var request = new LovedBookRequest();
            _lovedBookServiceMock.Setup(x => x.CreateLovedBook(request)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _lovedBookController.CreateLovedBook(request);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);

            Assert.False(response.Success);
            Assert.Equal("An error occurred", response.Message);
        }

        [Fact]
        public async Task DeleteLovedBook_WhenDeleteSucceeds_ReturnsOk()
        {
            // Arrange
            string userId = "user123";
            long bookId = 1;
            _lovedBookServiceMock.Setup(x => x.DeleteLovedBook(userId, bookId)).ReturnsAsync(true);

            // Act
            var result = await _lovedBookController.DeleteLovedBook(userId, bookId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal("Delete loved book successfully", response.Message);
        }

        [Fact]
        public async Task DeleteLovedBook_WhenDeleteFails_ReturnsConflict()
        {
            // Arrange
            string userId = "user123";
            long bookId = 1;
            _lovedBookServiceMock.Setup(x => x.DeleteLovedBook(userId, bookId)).ReturnsAsync(false);

            // Act
            var result = await _lovedBookController.DeleteLovedBook(userId, bookId);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);

            Assert.False(response.Success);
            Assert.Equal("Delete loved book failed", response.Message);
        }

        [Fact]
        public async Task DeleteLovedBook_WhenExceptionThrown_ReturnsConflict()
        {
            // Arrange
            string userId = "user123";
            long bookId = 1;
            _lovedBookServiceMock.Setup(x => x.DeleteLovedBook(userId, bookId)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _lovedBookController.DeleteLovedBook(userId, bookId);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            var response = Assert.IsType<GeneralResponse>(conflictResult.Value);

            Assert.False(response.Success);
            Assert.Equal("An error occurred", response.Message);
        }
    }
}