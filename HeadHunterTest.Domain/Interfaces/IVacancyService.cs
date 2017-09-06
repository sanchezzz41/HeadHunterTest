using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Models;

namespace HeadHunterTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с вакансией
    /// </summary>
    public interface IVacancyService
    {
        /// <summary>
        /// Список вакансий
        /// </summary>
        List<Vacancy> Vacancies { get; }

        /// <summary>
        /// Добавляет вакансию
        /// </summary>
        /// <param name="idEmployer"></param>
        /// <param name="vacancyModel"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid idEmployer,VacancyModel vacancyModel);

        /// <summary>
        /// Изменяет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        Task EditAsync(Guid idVacancy, VacancyModel newModel);

        /// <summary>
        /// Удаляет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid idVacancy);

        /// <summary>
        /// Возвращает список вакансий
        /// </summary>
        /// <returns></returns>
        Task<List<Vacancy>> GetAsync();

        /// <summary>
        /// Возвращает список резюме, которые закреплены за вакансией
        /// </summary>
        /// <param name="idVacancy">Id вакаснии, список резюме которой надо получить</param>
        /// <returns></returns>
        Task<List<Resume>> GetAttachmentsResumes(Guid idVacancy);
    }
}
