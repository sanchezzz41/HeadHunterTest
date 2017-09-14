using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.Tests.Domain.Factory
{
    /// <summary>
    /// Фабрика для заполнения бд проф. областями
    /// </summary>
    public class ProfAreaDataFactory
    {
        private readonly DatabaseContext _context;

        public ProfAreaDataFactory(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для добавления проф областей в хранилище(временное)
        /// </summary>
        /// <returns></returns>
        public async Task CreateProfArea()
        {
            var resultList = new List<ProfessionalArea>
            {
                new ProfessionalArea("It"),
                new ProfessionalArea("Врач")
            };
            await _context.ProfessionalAreas.AddRangeAsync(resultList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task Dispose()
        {
            var list = await _context.ProfessionalAreas.ToListAsync();
            _context.ProfessionalAreas.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
