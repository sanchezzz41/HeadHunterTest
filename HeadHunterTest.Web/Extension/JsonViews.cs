using HeadHunterTest.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HeadHunterTest.Web.Extension
{
    /// <summary>
    /// Статический класс предоставляющий методы расширения для отображения данных в виде Json
    /// </summary>
    public static class JsonViews
    {

        /// <summary>
        /// Метод расширения для отображения пользователей
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static object UserView(this User user)
        {
            if (user != null)
            {
                return new
                {
                    userId = user.Id,
                    user.Name,
                    user.PhoneNumber,
                    Role = nameof(user.RoleId)
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения города
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static object CityView(this City city)
        {
            if (city != null)
            {
                return new
                {
                    cityId = city.Id,
                    city.Name
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения професии
        /// </summary>
        /// <param name="prof"></param>
        /// <returns></returns>
        public static object ProfView(this ProfessionalArea prof)
        {
            if (prof != null)
            {
                return new
                {
                    profId = prof.Id,
                    prof.Name
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения резюме(для админов)
        /// </summary>
        /// <param name="resume"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object ResumeAdminView(this Resume resume, HttpContext context)
        {
            if (!context.User.IsInRole(nameof(RolesOptions.Admin)))
            {
                return null;
            }
            if (resume != null)
            {
                return new
                {
                    resumeid = resume.Id,
                    IdJobSeeker = resume.JobSeeker?.Id,
                    IdCity = resume.ResumeInCity?.Id,
                    IdProf = resume.ProfessionalArea?.Id,
                    resume.Salary,
                    resume.DesiredPosition
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения резюме
        /// </summary>
        /// <param name="resume"></param>
        /// <returns></returns>
        public static object ResumeView(this Resume resume)
        {
            if (resume != null)
            {
                return new
                {
                    resumeId = resume.Id,
                    resume.JobSeeker?.Name,
                    CityName = resume.ResumeInCity?.Name,
                    ProfName = resume.ProfessionalArea?.Name,
                    resume.Salary,
                    resume.DesiredPosition
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения вакансий
        /// </summary>
        /// <param name="vac"></param>
        /// <returns></returns>
        public static object VacancyView(this Vacancy vac)
        {
            if (vac != null)
            {
                return new
                {
                    vacancyId = vac.Id,
                    vac.Employer?.Name,
                    vac.Employer?.NameCompany,
                    vac.Description,
                    CityName = vac.VacanciesInCity?.Name
                };
            }
            return null;
        }
    }
}
