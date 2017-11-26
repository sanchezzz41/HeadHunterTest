using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.Tests.Domain.Factory
{
    /// <summary>
    /// Фабрика для заполнения бд резюме
    /// </summary>
    public class ResumeDataFactory
    {
        private readonly DatabaseContext _context;

        public ResumeDataFactory(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для добавления резюме в хранилище(временное)
        /// </summary>
        /// <returns></returns>
        public async Task CreateResume()
        {
            var jobSeeker = await _context.JobSeekers.FirstAsync();
            var city = await _context.Cities.LastAsync();
            var profArea = await _context.ProfessionalAreas.FirstAsync();
            var resultList = new List<Resume>
            {
                new Resume(jobSeeker.UserGuid, city.Id, profArea.Id, 20000, "Начальник"),
                new Resume(jobSeeker.UserGuid, city.Id, profArea.Id, 5000, "Уборщик")
            };
            await _context.Resumes.AddRangeAsync(resultList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task Dispose()
        {
            var list = await _context.Resumes.ToListAsync();
            _context.Resumes.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
