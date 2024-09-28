using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Dtos;
using AutoMapper;

namespace WarehouseManager.AutoMapperProfilesDtoModel;

public class BossDtoProfile : Profile
{
    public BossDtoProfile()
    {
        CreateMap<Boss, BossDto>();
        CreateMap<BossDto, Boss>();
    }
}