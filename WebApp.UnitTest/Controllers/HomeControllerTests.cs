using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApp.Controllers;
using WebApp.Repositories.Interfaces;
using Xunit;

namespace WebApp.UnitTest.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IBookRepository> _bookRepo = new();
        private readonly Mock<IAuthorRepository> _authorRepo = new();
        private readonly Mock<ILogger<HomeController>> _logger = new();
        private readonly Mock<IBookInstanceRepository> _bookInstanceRepo = new();
        
        
        [Theory]
        [InlineData(3,4,5,2)]
        [InlineData(12,23,24,15)]
        public async Task Index_WhenIsCalled_ShouldReturnViewResultWithCountingData
        (
            int bookCount,
            int authorCount,
            int bookInstanceCount,
            int bookInstanceAvailableCount
        )
        {
            //Arrange
            _bookRepo.Setup(repo => repo.GetCountAsync())
                .ReturnsAsync(bookCount);
            _authorRepo.Setup(repo => repo.GetCountAsync())
                .ReturnsAsync(authorCount);
            _bookInstanceRepo.Setup(repo => repo.GetCountAsync())
                .ReturnsAsync(bookInstanceCount);
            _bookInstanceRepo.Setup(repo => repo.GetCountAvailableAsync())
                .ReturnsAsync(bookInstanceAvailableCount);

            var controller = new HomeController(_logger.Object, _authorRepo.Object, _bookInstanceRepo.Object, _bookRepo.Object);

            var result = await controller.Index();
            Assert.IsType<ViewResult>(result);
            Assert.Equal(bookCount, controller.ViewData["numberOfBooks"]);
            Assert.Equal(authorCount,controller.ViewData["numberOfAuthors"]);
            Assert.Equal(bookInstanceCount,controller.ViewData["numberOfBookInstances"]);
            Assert.Equal(bookInstanceAvailableCount,controller.ViewData["numberOfAvailableInstances"]);
        }
    }
}