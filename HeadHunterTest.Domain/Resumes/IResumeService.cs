using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Resumes.Models;

namespace HeadHunterTest.Domain.Resumes
{
    /// <summary>
    /// Интерфейс для работы с резюме
    /// </summary>
    public interface IResumeService
    {
        /// <summary>
        /// Список резюме
        /// </summary>
        List<Resume> Resumes { get; }

        /// <summary>
        /// Добавляет новое резюме соискателя
        /// </summary>
        /// <param name="idJobSeeker">Id соискателя, которому будет добавлено резюме</param>
        /// <param name="model">Модель содержащая данные для резюме</param>
        /// <returns></returns>
        Task<Guid> AddAsync(Guid idJobSeeker, ResumeInfo model);

        /// <summary>
        /// Изменяет резюме
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо изменить</param>
        /// <param name="model">Модель для изменения резюме</param>
        /// <returns></returns>
        Task EditAsync(Guid idResume, ResumeInfo model);

        /// <summary>
        /// Удаляет резюме по Id
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо удалить</param>
        /// <returns></returns>
        Task DeleteAsync(Guid idResume);

        /// <summary>
        /// Возвращает список резюме
        /// </summary>
        /// <returns></returns>
       IQueryable<Resume> Get();

        /// <summary>
        /// Прикрепляет резюме к вакансии
        /// </summary>
        /// <param name="idResume">Id резюме</param>
        /// <param name="idVacancy">Id вакансии</param>
        /// <returns></returns>
        Task AttachResume(Guid idResume, Guid idVacancy);

        /// <summary>
        /// Возвращает список вакансий, которые прикриплены к резюме 
        /// </summary>
        /// <param name="idResume">Id резюме, вакансии которого будут возвращены</param>
        /// <returns></returns>
        Task<List<Vacancy>> GetAttachmentsVacancies(Guid idResume);

    }
}
