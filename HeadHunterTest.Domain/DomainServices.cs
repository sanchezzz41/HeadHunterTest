using HeadHunterTest.Domain.Authorizations;
using HeadHunterTest.Domain.Cities;
using HeadHunterTest.Domain.Notes;
using HeadHunterTest.Domain.ProfAreas;
using HeadHunterTest.Domain.Resumes;
using HeadHunterTest.Domain.Users;
using HeadHunterTest.Domain.Vacancies;
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
            service.AddScoped<INoteService, NoteService>();
            return service;
        }
    }
}
