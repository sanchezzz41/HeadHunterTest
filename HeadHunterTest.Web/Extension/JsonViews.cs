using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
                    userId = user.UserGuid,
                    user.Name,
                    PhoneNumber = user.Phone,
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
                    cityId = city.CityGuid,
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
                    profId = prof.ProfessionalAreaGuid,
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
                    resumeid = resume.ResumeGuid,
                    IdJobSeeker = resume.JobSeeker?.UserGuid,
                    IdCity = resume.ResumeInCity?.CityGuid,
                    IdProf = resume.ProfessionalArea?.ProfessionalAreaGuid,
                    resume.Salary,
                    DesiredPosition = resume.Position
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
                    resumeId = resume.ResumeGuid,
                    resume.JobSeeker?.Name,
                    CityName = resume.ResumeInCity?.Name,
                    ProfName = resume.ProfessionalArea?.Name,
                    resume.Salary,
                    DesiredPosition = resume.Position
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
                    vacancyId = vac.VacancyGuid,
                    vac.Employer?.Name,
                    NameCompany = vac.Employer?.NameOfCompany,
                    vac.Description,
                    CityName = vac.VacanciesInCity?.Name
                };
            }
            return null;
        }

        /// <summary>
        /// Возвращает коллекцию элементов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> GetCollection<T, TResult>(this IQueryable<T> query, Func<T, TResult> selector)
        {
            var list = await query.ToListAsync();
            return list.Select(selector);
        }

        public static async Task<IEnumerable<TResult>> GetCollection<T, TResult>(this IEnumerable<T> query, Func<T, TResult> selector)
        {
            var list = await Task.FromResult(query.ToList());
            return list.Select(selector);
        }

    }
}
