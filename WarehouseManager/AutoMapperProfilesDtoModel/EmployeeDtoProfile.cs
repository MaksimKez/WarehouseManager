using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Dtos;

namespace WarehouseManager.AutoMapperProfilesDtoModel;

public class EmployeeDtoProfile : Profile
{
    public EmployeeDtoProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}