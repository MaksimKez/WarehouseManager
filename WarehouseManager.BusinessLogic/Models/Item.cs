namespace WarehouseManager.BusinessLogic.Models;

public class Item
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsFragile { get; set; }
}