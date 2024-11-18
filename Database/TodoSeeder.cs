using TodoApplication.Database.Entities;

namespace TodoApplication.Database
{
	public class TodoSeeder()
	{
		private readonly TodoDbContext _dbcontext;

		public TodoSeeder(TodoDbContext dbcontext) : this()
		{
			this._dbcontext = dbcontext;
		}

		public void Seed()
		{

			if (!_dbcontext.Database.CanConnect() || _dbcontext.TodoItems.Any())
			{
				return;
			}

			var todos = GetTodos();
			_dbcontext.TodoItems.AddRange(todos);
			_dbcontext.SaveChanges();
		}

		private IEnumerable<TodoItem> GetTodos()
		{
			var sampleTodos = new List<TodoItem>
			{
				new TodoItem
				{
					Title = "Complete project proposal",
					Description = "Write and submit the project proposal document.",
					Date = DateTime.Today.AddDays(1),
					IsCompleted = false
				},
				new TodoItem
				{
					Title = "Grocery shopping",
					Description = "Buy ingredients for the weekend dinner.",
					Date = DateTime.Today.AddDays(2),
					IsCompleted = false
				},
				new TodoItem
				{
					Title = "Team meeting",
					Description = "Discuss project milestones and next steps.",
					Date = DateTime.Today,
					IsCompleted = false
				},
				new TodoItem
				{
					Title = "Exercise",
					Description = "Go for a 30-minute run in the evening.",
					Date = DateTime.Today.AddDays(3),
					IsCompleted = false
				}
			};
			return sampleTodos;
		}


	};
}
