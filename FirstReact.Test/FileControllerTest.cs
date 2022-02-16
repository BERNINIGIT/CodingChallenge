using FirstReact.Controllers;
using FirstReact.Core.Exceptions;
using FirstReact.Core.Models.Dtos;
using FirstReact.Core.Services.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FirstReact.Test
{
    public class FileControllerTest
    {
        private readonly FileController _fileController;
        private readonly ILogger<FileController> _logger;
        private readonly Mock<IFileProcessor> _fileProcessor;
        private readonly Mock<IFormFile> _fileMock;

        public FileControllerTest()
        {
            _logger = new Mock<ILogger<FileController>>().Object;
            _fileProcessor = new Mock<IFileProcessor>();
            _fileController = new FileController(_logger, _fileProcessor.Object);
            _fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task DealControllerReturnsOk()
        {
            _fileProcessor.Setup(s => s.ProcessCsvFile(It.IsAny<Stream>())).Returns(new List<CarSale>());
            var actionResult = await _fileController.Create(_fileMock.Object);
            actionResult.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task DealControllerReturnsBadRequest()
        {
            _fileProcessor.Setup(s => s.ProcessCsvFile(It.IsAny<Stream>())).Throws(new BadFileFormatException());
            var actionResult = await _fileController.Create(_fileMock.Object);
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task DealControllerReturnsServerError()
        {
            _fileProcessor.Setup(s => s.ProcessCsvFile(It.IsAny<Stream>())).Returns(new List<CarSale>());
            var actionResult = await _fileController.Create(It.IsAny<IFormFile>());
            (actionResult as ObjectResult).StatusCode.Should().Be(500);
        }
    }
}
