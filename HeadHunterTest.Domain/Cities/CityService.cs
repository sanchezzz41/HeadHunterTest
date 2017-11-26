using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Cities.Models;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Cities
{
    /// <summary>
    /// Класс реализиющий ICityService 
    /// </summary>
    public class CityService : ICityService
    {
        public List<City> Cities => _context.Cities.ToList();

        private readonly DatabaseContext _context;
        public CityService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет город по названию
        /// </summary>
        /// <param name="cityModel">Модель для добавления</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(CityInfo cityModel)
        {
            if (cityModel.Name == null || cityModel.Name.Length < 2)
            {
                throw new ArgumentException($"Название города{cityModel.Name} введено некорректно или равняется null.");
            }

            var resultNameCity = cityModel.Name;
            //Елси надо настроить регистр строки
            //resultNameCity = resultNameCity.ToUpper().First() + resultNameCity.Substring(1).ToLower();

            var resultCity = new City(resultNameCity);

            await _context.Cities.AddAsync(resultCity);
            await _context.SaveChangesAsync();

            return resultCity.CityGuid;
        }

        /// <summary>
        /// Изменяет название города
        /// </summary>
        /// <param name="id">Id города</param>
        /// <param name="newModel">Новая модель</param>
        /// <returns></returns>
        public async Task EditAsync(Guid id, CityInfo newModel)
        {
            if (newModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.CityGuid == id);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Города с {id} не существует.");
            }

            resultCity.Name = newModel.Name;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет город по id
        /// </summary>
        /// <param name="id">Id города</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.CityGuid == id);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Города с {id} не существует.");
            }
            _context.Cities.Remove(resultCity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Возвращает список городов
        /// </summary>
        /// <returns></returns>
        public async Task<List<City>> GetAsync()
        {
            return await _context.Cities
                .Include(x=>x.Users)
                .Include(x=>x.Resumes)
                .Include(x=>x.Vacancies)
                .ToListAsync();
        }
    }
}
