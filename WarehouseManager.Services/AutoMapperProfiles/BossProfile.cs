using AutoMapper;
using WarehouseManager.Database.Entities;
using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.AutoMapperProfiles;

public class BossProfile : Profile
{
    public BossProfile()
    {
        CreateMap<BossEntity, Boss>();
        CreateMap<Boss, BossEntity>();
    }
}