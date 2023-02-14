using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SirenValidity.Controllers;
using SirenValidity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SirenValidity.Interfaces;

namespace SirenValidityTests.Controllers
{
    public class SirenControllerTests
    {
        private readonly Mock<ILogger<SirenController>> _loggerMock;
        private readonly Mock<ISirenService> _sirenServiceMock;
        private readonly SirenController _controller;

        public SirenControllerTests()
        {
            _loggerMock = new Mock<ILogger<SirenController>>();
            _sirenServiceMock = new Mock<ISirenService>();
            _controller = new SirenController(_loggerMock.Object, _sirenServiceMock.Object);
        }

        [Fact]
        public async void CheckSirenValidity_ShouldReturnOk_WhenSirenIsValid()
        {
            // Arrange
            string siren = "123456782";
            bool expected = true;
            _sirenServiceMock.Setup(s => s.CheckSirenValidity(siren)).Returns(expected);

            // Act
            var result = await _controller.CheckSirenValidity(siren);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(okResult.Value);
            Assert.Equal(expected, (bool)okResult.Value);
        }

        [Fact]
        public async void CheckSirenValidity_ShouldReturnInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            string siren = "123456789";
            _sirenServiceMock.Setup(s => s.CheckSirenValidity(siren)).Throws(new Exception());

            // Act
            var result = await _controller.CheckSirenValidity(siren);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal("Erreur interne à l'application", objectResult.Value);
        }

        [Fact]
        public async void ComputeFullSiren_ShouldReturnOk_WhenSirenIsValid()
        {
            // Arrange
            string sirenWithoutControlNumber = "12345678";
            string expected = "123456782";
            _sirenServiceMock.Setup(s => s.ComputeFullSiren(sirenWithoutControlNumber)).Returns(expected);

            // Act
            var result = await _controller.ComputeFullSiren(sirenWithoutControlNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<string>(okResult.Value);
            Assert.Equal(expected, (string)okResult.Value);
        }

        [Fact]
        public async void ComputeFullSiren_ShouldReturnInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            var mockService = new Mock<ISirenService>();
            mockService.Setup(x => x.ComputeFullSiren(It.IsAny<string>())).Throws(new Exception("Error"));

            var controller = new SirenController(null, mockService.Object);

            // Act
            var result = await controller.ComputeFullSiren("123456789");

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal("Error", objectResult.Value);
        }
    }
}