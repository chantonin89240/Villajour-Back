namespace Villajour.UnitTest
{
    using Microsoft.VisualStudio.TestPlatform.TestHost;
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Villajour.Application.Commands.Users.AddUser;
    using Villajour.Application.Commands.Users.UpdateUser;
    using Villajour.Domain.Common;
    using Xunit;

    public class UserControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/User/{userId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<UserEntity>();
            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetUserById_InvalidId_ReturnsBadRequest()
        {
            // Act
            var response = await _client.GetAsync($"/api/User/{Guid.Empty}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("incorrect Guid.", responseString);
        }

        [Fact]
        public async Task AddUser_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new AddUserCommand
            {
                // Initialize properties
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/User", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<UserEntity>();
            Assert.NotNull(user);
        }

        [Fact]
        public async Task AddUser_NullCommand_ReturnsBadRequest()
        {
            // Act
            var response = await _client.PostAsJsonAsync("/api/User", null as AddUserCommand);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Command cannot be null.", responseString);
        }

        [Fact]
        public async Task UpdateUser_ValidCommand_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new UpdateUserCommand
            {
                Id = userId
                // Initialize other properties
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/User/{userId}", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<UserEntity>();
            Assert.NotNull(user);
        }

        [Fact]
        public async Task UpdateUser_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid()
                // Initialize other properties
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/User/{Guid.NewGuid()}", command);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("incorrect Guid.", responseString);
        }

        [Fact]
        public async Task DeleteUser_ValidId_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/User/{userId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteUser_InvalidId_ReturnsBadRequest()
        {
            // Act
            var response = await _client.DeleteAsync($"/api/User/{Guid.Empty}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("incorrect Guid.", responseString);
        }
    }
}