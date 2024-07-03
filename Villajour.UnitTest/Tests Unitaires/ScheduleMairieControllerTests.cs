namespace Villajour.UnitTest
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Villajour.API.Controllers;
    using Villajour.Application.Commands.Mairies.AddScheduleMairie;
    using Villajour.Domain.Common;
    using Xunit;

    public class ScheduleMairieControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ScheduleMairieController _controller;

        public ScheduleMairieControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ScheduleMairieController();
        }

        //[Fact]
        //public async Task Add_ValidCommand_ReturnsOk()
        //{
        //    // Arrange
        //    var command = new AddScheduleMairieCommand();
        //    var scheduleMairie = new ScheduleMairieEntity();
        //    _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(scheduleMairie);

        //    // Act
        //    var result = await _controller.AddSchedule(command);

        //    // Assert
        //    var okResult = Assert.IsType<ObjectResult>(result);
        //    Assert.Equal(scheduleMairie, okResult.Value);
        //}

        [Fact]
        public async Task Add_NullCommand_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.AddSchedule(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Command cannot be null.", badRequestResult.Value);
        }

        //[Fact]
        //public async Task Add_CommandNotFound_ReturnsNotFound()
        //{
        //    // Arrange
        //    var command = new AddScheduleMairieCommand();
        //    _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync((ScheduleMairieEntity)null);

        //    // Act
        //    var result = await _controller.AddSchedule(command);

        //    // Assert
        //    var notFoundResult = Assert.IsType<ObjectResult>(result);
        //    Assert.Equal("L'horaire de la mairie ne peut pas être ajouté", notFoundResult.Value);
        //}

        //[Fact]
        //public async Task Add_ThrowsException_ReturnsInternalServerError()
        //{
        //    // Arrange
        //    var command = new AddScheduleMairieCommand();
        //    _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        //    // Act
        //    var result = await _controller.AddSchedule(command);

        //    // Assert
        //    var statusCodeResult = Assert.IsType<ObjectResult>(result);
        //    Assert.Equal(500, statusCodeResult.StatusCode);
        //    Assert.Equal("Internal server error.", statusCodeResult.Value);
        //}
    }
}
