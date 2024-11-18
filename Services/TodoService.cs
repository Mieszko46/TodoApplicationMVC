using AutoMapper;
using TodoApplication.Database;
using TodoApplication.Database.Entities;
using TodoApplication.Models;

namespace TodoApplication.Services
{
	public class TodoService : ITodoService
	{
		private readonly TodoDbContext _todoDbContext;
		private readonly IMapper _mapper;

		public TodoService(TodoDbContext dbContext, IMapper mapper)
		{
			this._todoDbContext = dbContext;
			this._mapper = mapper;
		}
		public IEnumerable<TodoItemDto> GetTodos()
		{
			var todos = _todoDbContext
				.TodoItems
				.ToList();

			return _mapper.Map<List<TodoItemDto>>(todos);
		}

		public IEnumerable<TodoItemDto> GetUpcomingTodos()
		{
			var upcomingTodos = _todoDbContext.TodoItems
				.Where(t => t.Date >= DateTime.Today && t.Date <= DateTime.Today.AddDays(1) && !t.IsCompleted)
				.OrderBy(t => t.Date)
				.ToList();

			return _mapper.Map<List<TodoItemDto>>(upcomingTodos);
		}

		public IEnumerable<TodoItemDto> GetTodosByDate(DateTime date)
		{
			var todosOnSelectedDate = _todoDbContext.TodoItems
				.Where(t => t.Date.Date == date.Date) 
				.ToList();

			return _mapper.Map<List<TodoItemDto>>(todosOnSelectedDate);
		}


		public TodoItemDto GetTodoById(int id)
		{
			var todo = _todoDbContext.TodoItems.Find(id);
			return _mapper.Map<TodoItemDto>(todo);
		}

		public void CreateTodo(TodoItemDto todoDto)
		{
			var todoItem = _mapper.Map<TodoItem>(todoDto);
			_todoDbContext.TodoItems.Add(todoItem);       
			_todoDbContext.SaveChanges();
		}

		public void DeleteTodo(int id)
		{
			var todo = _todoDbContext.TodoItems.Find(id);
			if (todo != null)
			{
				_todoDbContext.TodoItems.Remove(todo);
				_todoDbContext.SaveChanges();
			}
		}

		public void EditTodo(TodoItemDto todoDto)
		{
			var todo = _todoDbContext.TodoItems.Find(todoDto.Id);
			if (todo != null)
			{
				todo.Title = todoDto.Title;
				todo.Description = todoDto.Description;
				todo.Date = todoDto.Date;
				todo.IsCompleted = todoDto.IsCompleted;

				_todoDbContext.SaveChanges();
			}
		}

	}
}
