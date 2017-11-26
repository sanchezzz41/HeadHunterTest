using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes.Models;
using HeadHunterTest.Domain.Vacancies.Models;

namespace HeadHunterTest.Domain.Vacancies
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
        /// <param name="employerGuid"></param>
        /// <param name="vacancyModel"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid employerGuid,VacancyInfo vacancyModel);

        /// <summary>
        /// Изменяет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        Task EditAsync(Guid idVacancy, VacancyInfo newModel);

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
        IQueryable<Vacancy> Get();

        Task AttachVacancy(NoteInfo model);

        /// <summary>
        /// Возвращает список резюме, которые закреплены за вакансией
        /// </summary>
        /// <param name="idVacancy">Id вакаснии, список резюме которой надо получить</param>
        /// <returns></returns>
        Task<List<Resume>> GetAttachmentsResumes(Guid idVacancy);
    }
}
