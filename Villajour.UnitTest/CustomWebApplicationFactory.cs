namespace Villajour.UnitTest
{

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Moq;
    using Villajour.Application.Commands.Users.AddUser;
    using Villajour.Application.Commands.Users.DeleteUser;
    using Villajour.Application.Commands.Users.GetUserById;
    using Villajour.Application.Commands.Users.UpdateUser;
    using Villajour.Domain.Common;
    using MediatR;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var mediatorMock = new Mock<IMediator>();

                // Setup mocks for different commands
                mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdCommand>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new UserEntity());
                mediatorMock.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new UserEntity());
                mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new UserEntity());
                mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(true);

                services.AddSingleton(mediatorMock.Object);
            });

            return base.CreateHost(builder);
        }
    }
}