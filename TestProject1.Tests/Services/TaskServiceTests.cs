using NSubstitute;
using Shouldly;
using TestProject1.Models.Entities;
using TestProject1.Repositories;
using TestProject1.Services;

namespace TestProject1.Tests.Services;

public class TaskServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsTasksFromRepository()
    {
        // Arrange
        var repository = Substitute.For<ITaskRepository>();

        IReadOnlyList<TaskItem> tasks =
        [
            new() { Title = "First task" },
            new() { Title = "Second task" }
        ];

        repository.GetAllAsync()
            .Returns(Task.FromResult(tasks));

        var service = new TaskService(repository);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Count.ShouldBe(2);
        result[0].Title.ShouldBe("First task");
        result[1].Title.ShouldBe("Second task");

        await repository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task CreateAsync_AddsTaskAndSavesChanges()
    {
        // Arrange
        var repository = Substitute.For<ITaskRepository>();
        var service = new TaskService(repository);

        // Act
        var result = await service.CreateAsync("Learn unit testing");

        // Assert
        result.Title.ShouldBe("Learn unit testing");
        result.IsCompleted.ShouldBeFalse();
        result.Id.ShouldNotBe(Guid.Empty);

        repository.Received(1).Add(
            Arg.Is<TaskItem>(t =>
                t.Title == "Learn unit testing"));

        await repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task CompleteAsync_WhenTaskExists_CompletesTask()
    {
        // Arrange
        var repository = Substitute.For<ITaskRepository>();

        var task = new TaskItem
        {
            Title = "Test task"
        };

        repository.GetByIdAsync(task.Id)
            .Returns(Task.FromResult<TaskItem?>(task));

        var service = new TaskService(repository);

        // Act
        var result = await service.CompleteAsync(task.Id);

        // Assert
        result.ShouldBeTrue();
        task.IsCompleted.ShouldBeTrue();

        await repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task CompleteAsync_WhenTaskDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var repository = Substitute.For<ITaskRepository>();
        var id = Guid.NewGuid();

        repository.GetByIdAsync(id)
            .Returns(Task.FromResult<TaskItem?>(null));

        var service = new TaskService(repository);

        // Act
        var result = await service.CompleteAsync(id);

        // Assert
        result.ShouldBeFalse();

        await repository.DidNotReceive().SaveChangesAsync();
    }
}
