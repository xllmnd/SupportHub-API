using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Application.Common.Behaviors;
using SupportHub.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
