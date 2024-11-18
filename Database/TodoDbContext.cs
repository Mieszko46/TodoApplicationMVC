using Microsoft.EntityFrameworkCore;
using TodoApplication.Database.Entities;

namespace TodoApplication.Database
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
	    public DbSet<TodoItem> TodoItems { get; set; }
	}
}
