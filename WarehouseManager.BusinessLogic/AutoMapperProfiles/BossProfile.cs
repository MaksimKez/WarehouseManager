using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.AutoMapperProfiles;

public class BossProfile : Profile
{
    public BossProfile()
    {
        CreateMap<BossEntity, Boss>();
        CreateMap<Boss, BossEntity>();
    }
}