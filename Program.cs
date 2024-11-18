using Microsoft.EntityFrameworkCore;
using TodoApplication.Database;
using TodoApplication.Services;

namespace TodoApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<TodoDbContext>(options =>
				options.UseInMemoryDatabase("TodoDatabase"));

			builder.Services.AddScoped<TodoSeeder>();

			builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<TodoSeeder>();
                seeder.Seed();
            }

            if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Todo/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Todo}/{action=Index}");

			app.Run();
		}
	}
}
