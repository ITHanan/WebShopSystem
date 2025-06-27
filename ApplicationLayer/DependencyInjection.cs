using ApplicationLayer.LanguageComAndQu.Commands;
using ApplicationLayer.LanguageComAndQu.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;


namespace ApplicationLayer
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SetUserLanguageCommand).Assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ApplicationLayer.Behaviors.ValidationBehavior<,>));

            return services;
        }

    }
}
