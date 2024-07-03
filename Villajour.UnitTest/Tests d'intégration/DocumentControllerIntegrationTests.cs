//using System.Net;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using Xunit;
//using Moq;
//using MediatR;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using Azure.Storage.Blobs;
//using Villajour.API.Controllers;
//using Villajour.Application.Commands.Documents.AddDocument;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Villajour.Application.Commands.Dto;

//public class DocumentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
//{
//    private readonly HttpClient _client;
//    private readonly WebApplicationFactory<Program> _factory;

//    public DocumentControllerIntegrationTests(WebApplicationFactory<Program> factory)
//    {
//        _factory = factory;

//        _client = factory.WithWebHostBuilder(builder =>
//        {
//            builder.ConfigureServices(services =>
//            {
//                var mediatorMock = new Mock<IMediator>();
//                var configMock = new Mock<IConfiguration>();

//                // Configuration setup
//                configMock.SetupGet(x => x["storage:connectionString"]).Returns("UseDevelopmentStorage=true");
//                configMock.SetupGet(x => x["storage:containerName"]).Returns("test-container");

//                // Register mocks
//                services.AddSingleton(mediatorMock.Object);
//                services.AddSingleton(configMock.Object);
//            });
//        }).CreateClient();
//    }

//    [Fact]
//    public async Task AddDocument_ReturnsOk()
//    {
//        // Arrange
//        var addDocumentDto = new AddDocumentDto
//        {
//            Title = "Test Document",
//            Description = "Test Description",
//            DocumentTypeId = 1,
//            MairieId = Guid.Parse("3fa85f64 - 5717 - 4562 - b3fc - 2c963f66afa6"),
//            Document = new FormFile(null, 0, 0, null, "test.pdf") // You may need to adjust this to simulate a file properly
//        };

//        // Act
//        var response = await _client.PostAsJsonAsync("/Api/Document", addDocumentDto);

//        // Assert
//        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//    }
//}
