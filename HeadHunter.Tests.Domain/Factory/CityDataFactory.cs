using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.Tests.Domain.Factory
{
    /// <summary>
    /// Фабрика для созд. начальных значений для городов
    /// </summary>
    public class CityDataFactory
    {
        private readonly DatabaseContext _context;

        public CityDataFactory(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для добавления городов в хранилище(временное)
        /// </summary>
        /// <returns></returns>
        public async Task CreateCities()
        {
               var resultList=new List<City>
               {
                   new City("Москва"),
                   new City("Киев")
               };
            await _context.Cities.AddRangeAsync(resultList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task Dispose()
        {
            var list = await _context.Cities.ToListAsync();
            _context.Cities.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
