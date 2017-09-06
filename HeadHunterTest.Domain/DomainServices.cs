using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Services;
using Microsoft.Extensions.DependencyInjection;


namespace HeadHunterTest.Domain
{
    /// <summary>
    /// Статический класс для добавления сервисов в колекцию
    /// </summary>
    public static class DomainServices
    {
        /// <summary>
        /// Метод расширения, добавляющий сервисы из domain в колекцию 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthorizationService, AuthorizationService>();
            service.AddScoped<ICityService, CityService>();
            service.AddScoped<IProfessionalAreaService, ProfessionalAreaService>();
            service.AddScoped<IResumeService, ResumeService>();
            service.AddScoped<IVacancyService, VacancyService>();
            return service;
        }
    }
}
