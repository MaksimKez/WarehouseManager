using AutoMapper;
using WarehouseManager.Database.Entities;
using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.AutoMapperProfiles;

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