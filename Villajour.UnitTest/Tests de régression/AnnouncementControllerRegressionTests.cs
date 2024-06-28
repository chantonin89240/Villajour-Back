using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Villajour.API.Controllers;
using Villajour.Application.Commands.Announcements.AddAnnouncement;
using Villajour.Domain.Common;
using System.Threading.Tasks;

public class AnnouncementControllerRegressionTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AnnouncementController _controller;

    public AnnouncementControllerRegressionTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AnnouncementController(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddAnnouncement_ValidCommand_ReturnsOk()
    {
        // Arrange
        var command = new AddAnnouncementCommand { /* Initialiser les propriétés nécessaires */ };
        var announcement = new AnnouncementEntity { /* Initialiser les propriétés nécessaires */ };

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddAnnouncementCommand>(), default)).ReturnsAsync(announcement);

        // Act
        var result = await _controller.AddAnnouncementt(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(announcement, okResult.Value);
    }

    [Fact]
    public async Task AddAnnouncement_CommandIsNull_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.AddAnnouncementt(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Command cannot be null.", badRequestResult.Value);
    }

    [Fact]
    public async Task AddAnnouncement_AnnouncementIsNull_ReturnsNotFound()
    {
        // Arrange
        var command = new AddAnnouncementCommand { /* Initialiser les propriétés nécessaires */ };

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddAnnouncementCommand>(), default)).ReturnsAsync((AnnouncementEntity)null);

        // Act
        var result = await _controller.AddAnnouncementt(command);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("L'annonce ne peut pas être ajoutée", notFoundResult.Value);
    }
}
