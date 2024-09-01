using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess;

public static class DataSeeder
{
    public static void EnsureSeedData( this ApplicationDatabaseContext context)
    {
        if (context.Bosses.Any() || context.Employees.Any() || context.Shelves.Any() || context.Items.Any() ||
            context.Todos.Any()) return;
        
        var bossId = Guid.NewGuid();
        context.Bosses.Add(new BossEntity
        {
            Id = bossId,
            Name = "John",
            Surname = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            CreatedAt = DateTime.UtcNow
        });

        var employee1Id = Guid.NewGuid();
        var employee2Id = Guid.NewGuid();
        context.Employees.AddRange(
            new EmployeeEntity
            {
                Id = employee1Id,
                Name = "Alice",
                Surname = "Smith",
                Email = "alice.smith@example.com",
                Password = "password123",
                Position = PositionEnum.Cleaner,
                IsFired = false,
                CreatedAt = DateTime.UtcNow
            },
            new EmployeeEntity
            {
                Id = employee2Id,
                Name = "Bob",
                Surname = "Johnson",
                Email = "bob.johnson@example.com",
                Password = "password123",
                Position = PositionEnum.WarehouseWorker,
                IsFired = false,
                CreatedAt = DateTime.UtcNow
            }
        );

        var shelf1Id = Guid.NewGuid();
        var shelf2Id = Guid.NewGuid();
        context.Shelves.AddRange(
            new ShelfEntity
            {
                Id = shelf1Id,
                Description = "Shelf 1"
            },
            new ShelfEntity
            {
                Id = shelf2Id,
                Description = "Shelf 2"
            }
        );

        var item1Id = Guid.NewGuid();
        var item2Id = Guid.NewGuid();
        context.Items.AddRange(
            new ItemEntity
            {
                Id = item1Id,
                Description = "Item 1",
                IsFragile = true
            },
            new ItemEntity
            {
                Id = item2Id,
                Description = "Item 2",
                IsFragile = false
            }
        );

        context.Todos.AddRange(
            new TodoEntity
            {
                Id = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Description for Task 1",
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                Deadline = DateTime.UtcNow.AddDays(7),
                EmployeeId = employee1Id,
                ShelfId = shelf1Id,
                ItemId = item1Id
            },
            new TodoEntity
            {
                Id = Guid.NewGuid(),
                Title = "Task 2",
                Description = "Description for Task 2",
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                Deadline = DateTime.UtcNow.AddDays(7),
                EmployeeId = employee2Id,
                ShelfId = shelf2Id,
                ItemId = item2Id
            }
        );

        context.SaveChanges();
    }
}
