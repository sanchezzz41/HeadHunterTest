using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.Tests.Domain.Factory
{
    /// <summary>
    /// Фабрика для добавления в бд вакансий
    /// </summary>
    public class VacancyDataFactory
    {
        private readonly DatabaseContext _context;

        public VacancyDataFactory(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для добавления вакансий в хранилище(временное)
        /// </summary>
        /// <returns></returns>
        public async Task CreateVacancy()
        {
            var emp = await _context.Employers.FirstAsync();
            var city = await _context.Cities.LastAsync();
            var resultList = new List<Vacancy>
            {
                new Vacancy(emp.Id,city.Id,"Нужен IT"),
                new Vacancy(emp.Id,city.Id,"Нужен Врач")
            };
            await _context.Vacancies.AddRangeAsync(resultList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task Dispose()
        {
            var list = await _context.Vacancies.ToListAsync();
            _context.Vacancies.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
