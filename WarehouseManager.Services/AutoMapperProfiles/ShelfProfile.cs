using AutoMapper;
using WarehouseManager.Database.Entities;
using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.AutoMapperProfiles;

public class ShelfProfile : Profile
{
    public ShelfProfile()
    {
        CreateMap<Shelf, ShelfEntity>();
        CreateMap<ShelfEntity, Shelf>();
    }
}