using AutoMapper;
using WarehouseManager.Database.Entities;
using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.AutoMapperProfiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<ItemEntity, Item>();
        CreateMap<Item, ItemEntity>();
    }
}