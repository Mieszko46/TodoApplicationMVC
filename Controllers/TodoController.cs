using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApplication.Models;
using TodoApplication.Services;

namespace TodoApplication.Controllers
{
	public class TodoController : Controller
	{
		private readonly ILogger<TodoController> _logger;
		private readonly ITodoService _todoService;

		public TodoController(ILogger<TodoController> logger, ITodoService service)
		{
			_logger = logger;
			_todoService = service;
		}

		public IActionResult Index(DateTime? filterDate)
		{
			// Retrieve upcoming tasks
			var upcomingTodos = _todoService.GetUpcomingTodos();

			// If a filter date is provided, apply the filter; otherwise, get all todos
			IEnumerable<TodoItemDto> todos;

			if (filterDate.HasValue)
			{
				// Filter todos by the selected date
				todos = _todoService.GetTodosByDate(filterDate.Value);
			}
			else
			{
				// Get all todos if no date filter is provided
				todos = _todoService.GetTodos();
			}

			var viewModel = new TodoIndexViewModel
			{
				Todos = todos,
				UpcomingTodos = upcomingTodos,
				FilterDate = filterDate
			};

			return View(viewModel);
		}



		public IActionResult UpcomingTodos()
		{
			var upcomingTodos = _todoService.GetUpcomingTodos();
			return View(upcomingTodos);
		}


		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(TodoItemDto todoDto)
		{
			if (ModelState.IsValid)
			{
				_todoService.CreateTodo(todoDto);
				return RedirectToAction("Index");
			}
			return View(todoDto);
		}

		public IActionResult Delete(int id)
		{
			var todo = _todoService.GetTodoById(id);
			if (todo == null)
			{
				return NotFound();
			}
			return View(todo);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			_todoService.DeleteTodo(id);
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Edit(int id)
		{
			var todo = _todoService.GetTodoById(id);
			if (todo == null)
			{
				return NotFound();
			}

			return View(todo);
		}

		[HttpPost]
		public IActionResult Edit(int id, TodoItemDto todoDto)
		{
			if (id != todoDto.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_todoService.EditTodo(todoDto);
				return RedirectToAction("Index");
			}
			return View(todoDto); 
		}
	}
}
