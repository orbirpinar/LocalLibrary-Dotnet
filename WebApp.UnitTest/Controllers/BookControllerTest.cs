using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Repositories.Interfaces;
using Xunit;

namespace WebApp.UnitTest.Controllers
{
    public class BookControllerTest
    {
        private readonly BookController _sut;
        private readonly Mock<IBookRepository> _mockBookRepo = new();
        private readonly Mock<IAuthorRepository> _mockAuthorRepo = new();
        private readonly Mock<ILanguageRepository> _mockLanguageRepo = new();

        public BookControllerTest()
        {
            _sut = new BookController(_mockBookRepo.Object, _mockAuthorRepo.Object, _mockLanguageRepo.Object);
        }

        [Fact]
        public async Task Index_WhenIsCalled_ShouldReturnViewWithBookList()
        {
            // Arrange
            _mockBookRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllBooksTest());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            viewResult.Model.Should().BeEquivalentTo(GetAllBooksTest());
        }

        [Fact]
        public async Task Detail_WhenBookIsNull_ShouldReturnNotFound()
        {
            // Arrange
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book?) null);

            // Adt
            var result = await _sut.Detail(2);
            
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            notFoundResult.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task Detail_WhenBookDoesNotNull_ShouldReturnViewResultWithBookObject()
        {
            // Arrange
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(GetOneBook());
            
            // Act
            var result = await _sut.Detail(1);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            viewResult.Model.Should().BeEquivalentTo(GetOneBook());
        }

        [Fact]
        public async Task CreateGet_WhenIsCalled_ShouldReturnViewResultWithAuthorsAndLanguagesSelectList()
        {
            // Arrange
            _mockAuthorRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllAuthorsTest());
            _mockLanguageRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllLanguagesTest());
            
            // Act
            var result = await _sut.Create();
            var authorSelectList = _sut.ViewBag.Authors as SelectList;
            var languageSelectList = _sut.ViewBag.Languages as SelectList;
            
            // Assert
            Assert.IsType<ViewResult>(result);
            authorSelectList.Should().NotBeNull();
            languageSelectList.Should().NotBeNull();
        }

        private static IEnumerable<Book> GetAllBooksTest()
        {
            Book book1 = new()
            {
                Id = 1,
                Title = "Fake title",
                Summary = "Fake summary"
            };

            Book book2 = new()
            {
                Id = 2,
                Title = "Fake title",
                Summary = "Fake summary"
            };
            List<Book> books = new()
            {
                book1,
                book2
            };
            return books;
        }

        private static Book GetOneBook()
        {
            return new Book
            {
                Id = 1,
                Title = "Fake title",
                Summary = "Fake summary"
            };
        }

        private static IEnumerable<Author> GetAllAuthorsTest()
        {
            Author author1 = new()
            {
                Id = 1,
                FirstName = "Fyodor",
                LastName = "Dostoevsky"
            };

            Author author2 = new()
            {
                Id = 2,
                FirstName = "Lev",
                LastName = "Tolstoy"
            };

            return new List<Author>
            {
                author1,
                author2
            };
        }

        private static IEnumerable<Language> GetAllLanguagesTest()
        {
            Language language1 = new()
            {
                Id = 1,
                Name = "English"
            };

            Language language2 = new()
            {
                Id = 1,
                Name = "Turkish"
            };

            return new List<Language>
            {
                language1,
                language2
            };
        }
    }
}