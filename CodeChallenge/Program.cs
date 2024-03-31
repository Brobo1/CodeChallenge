using CodeChallenge.Data;

using Microsoft.EntityFrameworkCore;

namespace CodeChallenge;

public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<IApplicationDbContext, CodeChallengeDbContext>(
			options => options.UseSqlite(builder.Configuration.GetConnectionString("CodeChallengeDbContext")));

		builder.Services.AddControllers().AddXmlSerializerFormatters();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();
		app.UseSwagger();
		app.UseSwaggerUI();
		
		app.UseCors(c => {
			c
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin();
		});
		
		app.MapControllers();

		app.Run();
	}
}