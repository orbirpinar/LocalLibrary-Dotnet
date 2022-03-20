using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Repositories.Interfaces;
using Xunit;

namespace WebApp.UnitTest.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _sut;
        private readonly Mock<IBookRepository> _bookRepo = new();
        private readonly Mock<IAuthorRepository> _authorRepo = new();
        private readonly Mock<IBookInstanceRepository> _bookInstanceRepo = new();

        public HomeControllerTests()
        {
            _sut = new HomeController(_authorRepo.Object, _bookInstanceRepo.Object, _bookRepo.Object);
        }
        
        
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


            //Act
            var result = await _sut.Index();
            
            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(bookCount, _sut.ViewData["numberOfBooks"]);
            Assert.Equal(authorCount,_sut.ViewData["numberOfAuthors"]);
            Assert.Equal(bookInstanceCount,_sut.ViewData["numberOfBookInstances"]);
            Assert.Equal(bookInstanceAvailableCount,_sut.ViewData["numberOfAvailableInstances"]);
        }
    }
}