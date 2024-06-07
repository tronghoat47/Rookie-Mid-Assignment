using AutoMapper;
using BaseProject.Application.Constants;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Application.Services.Impl;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.Unit.Services
{
    public class BookServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BookService _bookService;

        public BookServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _bookService = new BookService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBook_WithNullImage_ReturnsTrue()
        {
            // Arrange
            var bookRequest = new BookRequest
            {
                Name = "Sample Book",
                Author = "Sample Author",
                Description = "Sample Description",
                ReleaseYear = 2022,
                CategoryId = 1,
                DaysForBorrow = 14
            };

            _unitOfWorkMock.Setup(u => u.BookRepository.AddAsync(It.IsAny<Book>()));
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _bookService.CreateBook(bookRequest);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.BookRepository.AddAsync(It.IsAny<Book>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteBook_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int bookId = 1;
            _unitOfWorkMock.Setup(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync((Book)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _bookService.DeleteBook(bookId);
            });

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task GetBook_WithValidId_ReturnsBookResponse()
        {
            // Arrange
            int bookId = 1;
            var book = new Book
            {
                Id = bookId,
                Name = "Sample Book",
                Category = new Category { Id = 1, Name = "Sample Category" },
                LovedBooks = new List<LovedBook>(),
                Ratings = new List<Rating>(),
                Comments = new List<Comment>()
            };

            var expectedResponse = new BookResponse
            {
                Id = bookId,
                Name = "Sample Book",
                CategoryId = 1,
                CategoryName = "Sample Category",
                CountLoved = 0
            };

            _unitOfWorkMock.Setup(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync(book);

            _mapperMock.Setup(m => m.Map<BookResponse>(It.IsAny<Book>()))
                .Returns(expectedResponse);

            // Act
            var result = await _bookService.GetBook(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Name, result.Name);
            Assert.Equal(expectedResponse.CategoryId, result.CategoryId);
            Assert.Equal(expectedResponse.CategoryName, result.CategoryName);
            Assert.Equal(expectedResponse.CountLoved, result.CountLoved);

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _mapperMock.Verify(m => m.Map<BookResponse>(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task GetBook_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int bookId = 1;
            _unitOfWorkMock.Setup(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.GetBook(bookId);

            // Assert
            Assert.Null(result);

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _mapperMock.Verify(m => m.Map<BookResponse>(It.IsAny<Book>()), Times.Never);
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBookResponse()
        {
            // Arrange
            var books = new List<Book>
    {
        new Book { Id = 1, Name = "Book 1" },
        new Book { Id = 2, Name = "Book 2" }
    };

            var expectedResponse = books.Select(b => new BookResponse { Id = b.Id, Name = b.Name });

            _unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync(books);

            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()))
                .Returns(expectedResponse);

            // Act
            var result = await _bookService.GetBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count(), result.Count());

            foreach (var expectedBookResponse in expectedResponse)
            {
                var actualBookResponse = result.FirstOrDefault(b => b.Id == expectedBookResponse.Id);
                Assert.NotNull(actualBookResponse);
                Assert.Equal(expectedBookResponse.Name, actualBookResponse.Name);
            }

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _mapperMock.Verify(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()), Times.Once);
        }

        [Fact]
        public async Task GetTop10NewsBook_ReturnsListOfTop10NewBookResponse()
        {
            // Arrange
            var books = new List<Book>
    {
        new Book { Id = 1, Name = "Book 1", ReleaseYear = 2022 },
        new Book { Id = 2, Name = "Book 2", ReleaseYear = 2023 },
        // Add more books as needed
    };

            var expectedTop10NewBooks = books.OrderByDescending(b => b.ReleaseYear).Take(10)
                .Select(b => new BookResponse { Id = b.Id, Name = b.Name, ReleaseYear = b.ReleaseYear });

            _unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync(books);

            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()))
                .Returns<IEnumerable<Book>>(b => b.Select(book => new BookResponse { Id = book.Id, Name = book.Name, ReleaseYear = book.ReleaseYear }));

            // Act
            var result = await _bookService.GetTop10NewsBook();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTop10NewBooks.Count(), result.Count());

            foreach (var expectedBook in expectedTop10NewBooks)
            {
                var actualBook = result.FirstOrDefault(b => b.Id == expectedBook.Id);
                Assert.NotNull(actualBook);
                Assert.Equal(expectedBook.Name, actualBook.Name);
                Assert.Equal(expectedBook.ReleaseYear, actualBook.ReleaseYear);
            }

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _mapperMock.Verify(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()), Times.Once);
        }

        [Fact]
        public async Task GetTop10LovedBook_ReturnsListOfTop10LovedBookResponse()
        {
            // Arrange
            var books = new List<Book>
    {
        new Book { Id = 1, Name = "Book 1", LovedBooks = new List<LovedBook>() },
        new Book { Id = 2, Name = "Book 2", LovedBooks = new List<LovedBook> { new LovedBook(), new LovedBook() } },
        // Add more books as needed
    };

            var expectedTop10LovedBooks = books.OrderByDescending(b => b.LovedBooks.Count).Take(10)
                .Select(b => new BookResponse { Id = b.Id, Name = b.Name });

            _unitOfWorkMock.Setup(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()))
                .ReturnsAsync(books);

            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()))
                .Returns<IEnumerable<Book>>(b => b.Select(book => new BookResponse { Id = book.Id, Name = book.Name }));

            // Act
            var result = await _bookService.GetTop10LovedBook();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTop10LovedBooks.Count(), result.Count());

            foreach (var expectedBook in expectedTop10LovedBooks)
            {
                var actualBook = result.FirstOrDefault(b => b.Id == expectedBook.Id);
                Assert.NotNull(actualBook);
                Assert.Equal(expectedBook.Name, actualBook.Name);
            }

            _unitOfWorkMock.Verify(u => u.BookRepository.GetAllAsync(
                It.IsAny<Expression<Func<Book, bool>>>(),
                It.IsAny<Expression<Func<Book, object>>[]>()
            ), Times.Once);

            _mapperMock.Verify(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<IEnumerable<Book>>()), Times.Once);
        }

        [Fact]
        public async Task SaveFileAsync_WithInvalidFile_ReturnsFalse()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(0);

            // Act
            var result = await _bookService.SaveFileAsync(fileMock.Object);

            // Assert
            Assert.False(result);
        }
    }
}