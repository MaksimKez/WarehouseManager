using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.AutoMapperProfiles;

public class ShelfProfile : Profile
{
    public ShelfProfile()
    {
        CreateMap<Shelf, ShelfEntity>();
        CreateMap<ShelfEntity, Shelf>();
    }
}