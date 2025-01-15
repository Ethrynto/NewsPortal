using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using API.Controllers;
using Microsoft.Extensions.Logging;
using Assert = Xunit.Assert;

namespace Tests
{
    public class MediafilesControllerTests
    {
        private readonly Mock<IMediafilesService> _mockService;
        private readonly Mock<ILogger<MediafilesController>> _mockLogger;
        private readonly MediafilesController _controller;

        public MediafilesControllerTests()
        {
            _mockService = new Mock<IMediafilesService>();
            _mockLogger = new Mock<ILogger<MediafilesController>>();
            _controller = new MediafilesController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetImages_ReturnsOkResult_WithListOfMediafiles()
        {
            // Arrange
            var mediafiles = new List<Mediafile> { new Mediafile { Id = Guid.NewGuid(), FileName = "test.jpg" } };
            _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(mediafiles);

            // Act
            var result = await _controller.GetImages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Mediafile>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetImage_ReturnsOkResult_WithMediafile()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mediafile = new Mediafile { Id = id, FileName = "test.jpg" };
            _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(mediafile);

            // Act
            var result = await _controller.GetImage(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Mediafile>(okResult.Value);
            Assert.Equal(id, returnValue.Id);
        }

        [Fact]
        public async Task GetImage_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync((Mediafile)null!);

            // Act
            var result = await _controller.GetImage(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostImage_ReturnsBadRequest_ForNullFile()
        {
            // Act
            var result = await _controller.PostImage(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid file.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostImage_ReturnsOkResult_WithMediafile()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var content = "Fake file content";
            var fileName = "test.jpg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(stream);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(stream.Length);
            fileMock.Setup(_ => _.ContentType).Returns("image/jpeg");

            _mockService.Setup(service => service.AddAsync(fileMock.Object))
                .ReturnsAsync(new Mediafile { Id = Guid.NewGuid(), FileName = fileName });

            // Act
            var result = await _controller.PostImage(fileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Mediafile>(okResult.Value);
        }

        [Fact]
        public async Task PutImage_ReturnsBadRequest_ForMismatchedId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mediafile = new Mediafile { Id = Guid.NewGuid(), FileName = "test.jpg" };

            // Act
            var result = await _controller.PutImage(id, mediafile);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutImage_ReturnsNoContentResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mediafile = new Mediafile { Id = id, FileName = "test.jpg" };
            _mockService.Setup(service => service.UpdateAsync(mediafile)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutImage(id, mediafile);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteImage_ReturnsNoContentResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteImage(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
