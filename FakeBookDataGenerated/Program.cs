
using FakeBookDataGenerated.Service;

namespace FakeBookDataGenerated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<GeneratorService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(builder.Configuration.GetSection("CorsSettings:CorsPolicyName").Value,
                    CorsBuilder => CorsBuilder
                        .WithOrigins($"{builder.Configuration.GetSection("CorsSettings:URL").Value}")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        );
            });

            var app = builder.Build();

            app.UseCors(builder.Configuration.GetSection("CorsSettings:CorsPolicyName").Value);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
