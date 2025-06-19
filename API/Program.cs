
using ApplicationLayer;
using InfrastructureLayer;
using InfrastructureLayer.Extensions;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            // Add services from Application and Infrastructure
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddJwtAuthentication(builder.Configuration);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerWithJwt();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthentication(); // must come before UseAuthorization
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}



