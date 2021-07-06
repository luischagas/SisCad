using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IHealthInsuranceRepository, HealthInsuranceRepository>();
            //services.AddScoped<IHealthInsuranceService, HealthInsuranceService>();
            //services.AddScoped<IAttendantRepository, AttendantRepository>();
            //services.AddScoped<IAttendantService, AttendantService>();
            //services.AddScoped<IPatientRepository, PatientRepository>();
            //services.AddScoped<IPatientService, PatientService>();
            //services.AddScoped<ISchedulingRepository, SchedulingRepository>();
            //services.AddScoped<ISchedulingService, SchedulingService>();

            return services;
        }

        #endregion Methods
    }
}