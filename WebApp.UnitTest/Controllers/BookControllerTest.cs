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
        private readonly Mock<IBookRepository> _mockBookRepo = new();
        private readonly Mock<IAuthorRepository> _mockAuthorRepo = new();
        private readonly Mock<ILanguageRepository> _mockLanguageRepo = new();

        [Fact]
        public async Task Index_WhenIsCalled_ShouldReturnViewWithBookList()
        {
            _mockBookRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllBooksTest());
            var controller = new BookController(_mockBookRepo.Object,_mockAuthorRepo.Object,_mockLanguageRepo.Object);
            var result = await controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            viewResult.Model.Should().BeEquivalentTo(GetAllBooksTest());
        }
        
        [Fact]
        public async Task Detail_WhenBookIsNull_ShouldReturnNotFound()
        {
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book?) null);
            var controller = new BookController(_mockBookRepo.Object,_mockAuthorRepo.Object,_mockLanguageRepo.Object);
            var result = await controller.Detail(2);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            notFoundResult.StatusCode.Should().Be(404);
        }
 

        [Fact]
        public async Task Detail_WhenBookDoesNotNull_ShouldReturnViewResultWithBookObject()
        {
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(GetOneBook());
            var controller = new BookController(_mockBookRepo.Object,_mockAuthorRepo.Object,_mockLanguageRepo.Object);
            var result = await controller.Detail(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            viewResult.Model.Should().BeEquivalentTo(GetOneBook());
        }

        [Fact]
        public async Task CreateGet_WhenIsCalled_ShouldReturnViewResultWithAuthorsAndLanguagesSelectList()
        {
            _mockAuthorRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllAuthorsTest());
            _mockLanguageRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetAllLanguagesTest());
            var controller = new BookController(_mockBookRepo.Object,_mockAuthorRepo.Object,_mockLanguageRepo.Object);
            var result = await controller.Create();
            Assert.IsType<ViewResult>(result);
            var authorSelectList = controller.ViewBag.Authors as SelectList;
            var languageSelectList = controller.ViewBag.Languages as SelectList;
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