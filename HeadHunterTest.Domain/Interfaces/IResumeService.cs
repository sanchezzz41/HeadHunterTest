using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Models;

namespace HeadHunterTest.Domain.Interfaces
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
        Task<Guid> AddAsync(Guid idJobSeeker, ResumeModel model);

        /// <summary>
        /// Изменяет резюме
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо изменить</param>
        /// <param name="model">Модель для изменения резюме</param>
        /// <returns></returns>
        Task EditAsync(Guid idResume, ResumeModel model);

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
        Task<List<Resume>> GetAsync();

    }
}
