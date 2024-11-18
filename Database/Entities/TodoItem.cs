using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TodoApplication.Database.Entities
{
	public class TodoItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(250)]
		public string Description { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public bool IsCompleted { get; set; }
	}
}
