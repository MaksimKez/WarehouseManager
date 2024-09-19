using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.AutoMapperProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeEntity, Employee>();

        CreateMap<Employee, EmployeeEntity>()
            .ForMember(dest => dest.Todos, opt => opt.Ignore());
    }
}