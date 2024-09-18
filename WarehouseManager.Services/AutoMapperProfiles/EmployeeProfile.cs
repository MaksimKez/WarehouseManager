using AutoMapper;
using WarehouseManager.Database.Entities;
using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.AutoMapperProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeEntity, Employee>();

        CreateMap<Employee, EmployeeEntity>()
            .ForMember(dest => dest.Todos, opt => opt.Ignore());
    }
}