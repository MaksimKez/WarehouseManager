using AutoMapper;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.AutoMapperProfiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoEntity, Todo>();
        CreateMap<Todo, TodoEntity>().ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Item, opt => opt.Ignore())
            .ForMember(dest => dest.Shelf, opt => opt.Ignore());
    }
}