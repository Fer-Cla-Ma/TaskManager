using FluentAssertions;
using Moq;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Tests
{
    public class TaskServicesTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskItemService _taskService;

        public TaskServicesTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskItemService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTaskResponse_WhenValidRequest()
        {
            // Arrange
            var request = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Nueva tarea",
                Status = TaskItemStatus.Pending,
            };
            

            // Act
            var result = await _taskService.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Title, result.Title);
            Assert.Equal(TaskItemStatus.Pending, result.Status);
            _taskRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowValidationException_WhenTitleIsEmpty()
        {
            // Arrange
            var request = new TaskItem
            {
                Title = "", // <-- inválido
                Description = "Valid description",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            Func<Task> act = async () => await _taskService.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>()
                .WithMessage("*Title*");
        }
    }
}
