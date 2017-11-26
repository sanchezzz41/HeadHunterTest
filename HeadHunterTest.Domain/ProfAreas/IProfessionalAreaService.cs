using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.ProfAreas.Models;

namespace HeadHunterTest.Domain.ProfAreas
{
    /// <summary>
    /// Интерфейс для работы с проффесиями
    /// </summary>
    public interface IProfessionalAreaService
    {
        /// <summary>
        /// Список професий
        /// </summary>
        List<ProfessionalArea> ProfessionalAreas { get; }

        /// <summary>
        /// Название професии
        /// </summary>
        /// <param name="profModel">Модель которая из которой будут браться данные</param>
        /// <returns></returns>
        Task<Guid> AddAsync(ProfessionalAreaInfo profModel);

        /// <summary>
        /// Изменяет название професии
        /// </summary>
        /// <param name="id">Id професии</param>
        /// <param name="newModel">Новая модель, из которой будут браться данные</param>
        /// <returns></returns>
        Task EditAsync(Guid id, ProfessionalAreaInfo newModel);

        /// <summary>
        /// Удаляет професию по id
        /// </summary>
        /// <param name="id">Id професии</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Возвращает список професий
        /// </summary>
        /// <returns></returns>
        Task<List<ProfessionalArea>> GetAsync();
    }
}
