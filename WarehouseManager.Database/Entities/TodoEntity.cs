namespace WarehouseManager.Database.Entities;

public class TodoEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Deadline { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public Guid ShelfId { get; set; }
    public ShelfEntity Shelf { get; set; }
    public Guid ItemId { get; set; }
    public ItemEntity Item { get; set; }
}