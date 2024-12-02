using Quiz.Components;

namespace Quiz
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddHttpClient();
			builder.Services.AddControllers();
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseStaticFiles();
			app.UseAntiforgery();
			app.MapControllers();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			GlobalData.AddQuestions();

			app.Run();
		}
	}
}
