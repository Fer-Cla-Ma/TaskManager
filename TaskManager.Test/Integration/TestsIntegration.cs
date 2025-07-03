using System.Net.Http.Json;
using FluentAssertions;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests.Integration;

public class TasksControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TasksControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostTask_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var request = new TaskItem
        {
            Title = "Test task",
            Description = "Integration test",
            DueDate = DateTime.UtcNow.AddDays(3)
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Task", request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        var task = await response.Content.ReadFromJsonAsync<TaskItem>();
        task.Should().NotBeNull();
        task!.Title.Should().Be("Test task");
    }
}
