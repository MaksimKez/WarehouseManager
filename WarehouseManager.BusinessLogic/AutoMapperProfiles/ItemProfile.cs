using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.AutoMapperProfiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<ItemEntity, Item>();
        CreateMap<Item, ItemEntity>();
    }
}