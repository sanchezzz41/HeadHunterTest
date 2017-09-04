﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Models;

namespace HeadHunterTest.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с городами
    /// </summary>
    public interface ICityService
    {
        List<City> Cities { get; }

        /// <summary>
        /// Добавляет город по названию
        /// </summary>
        /// <param name="cityModel">Модель для добавления</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CityModel cityModel);

        /// <summary>
        /// Изменяет название города
        /// </summary>
        /// <param name="id">Id города</param>
        /// <param name="newModel">Новая модель</param>
        /// <returns></returns>
        Task EditAsync(Guid id, CityModel newModel);

        /// <summary>
        /// Удаляет город по id
        /// </summary>
        /// <param name="id">Id города</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Возвращает список городов
        /// </summary>
        /// <returns></returns>
        Task<List<City>> GetAsync();
    }
}
