using AutoMapper;
using TodoApplication.Database.Entities;
using TodoApplication.Models;

namespace TodoApplication
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            CreateMap<TodoItemDto, TodoItem>();
        }
    }
}
