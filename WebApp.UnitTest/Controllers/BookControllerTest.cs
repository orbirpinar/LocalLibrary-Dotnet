using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    }
}