namespace TodoApplication.Models
{
	public class TodoIndexViewModel
	{
		public IEnumerable<TodoItemDto> Todos { get; set; }
		public IEnumerable<TodoItemDto> UpcomingTodos { get; set; }
		public DateTime? FilterDate { get; set; }
	}
}