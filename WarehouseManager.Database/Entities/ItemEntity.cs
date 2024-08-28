namespace WarehouseManager.Database.Entities;

public class ItemEntity
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsFragile { get; set; }
}