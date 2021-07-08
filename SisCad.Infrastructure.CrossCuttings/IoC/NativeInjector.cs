using Microsoft.Extensions.DependencyInjection;
using SisCad.Application.Interfaces;
using SisCad.Application.Services;
using SisCad.Domain.Interfaces.Notification;
using SisCad.Domain.Interfaces.Repositories;
using SisCad.Domain.Interfaces.Services;
using SisCad.Domain.Notification;
using SisCad.Domain.Shared;
using SisCad.Infrastructure.Context;
using SisCad.Infrastructure.CrossCutting.Services;
using SisCad.Infrastructure.Repositories;

namespace SisCad.Infrastructure.CrossCutting.IoC
{
    public static class NativeInjector
    {
        #region Methods

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SisCadContext>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IClienteService, ClientService>();
            services.AddScoped<IContactService, ContactService>();

            return services;
        }

        #endregion Methods
    }
}