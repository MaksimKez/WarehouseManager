namespace WarehouseManager.Services.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Deadline { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ShelfId { get; set; }
    public Guid ItemId { get; set; }
}