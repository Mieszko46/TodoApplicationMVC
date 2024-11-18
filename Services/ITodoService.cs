using TodoApplication.Models;

namespace TodoApplication.Services
{
	public interface ITodoService
	{
		public IEnumerable<TodoItemDto> GetTodos();
		public IEnumerable<TodoItemDto> GetUpcomingTodos();
		public IEnumerable<TodoItemDto> GetTodosByDate(DateTime date);
		TodoItemDto GetTodoById(int id);
		public void CreateTodo(TodoItemDto todoDto);
		public void EditTodo(TodoItemDto todoDto);
		void DeleteTodo(int id);
	}
}
