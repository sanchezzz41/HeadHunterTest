using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Services
{
    /// <summary>
    /// Класс реализующий интерфейс IProfessionalAreaService
    /// </summary>
    public class ProfessionalAreaService : IProfessionalAreaService
    {
        /// <summary>
        /// Список професий
        /// </summary>
        public List<ProfessionalArea> ProfessionalAreas => _context.ProfessionalAreas.ToList();

        private readonly DatabaseContext _context;

        public ProfessionalAreaService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Название професии
        /// </summary>
        /// <param name="profModel">Модель которая из которой будут браться данные</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(ProfessionalAreaModel profModel)
        {
            if (profModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultProf = new ProfessionalArea(profModel.Name);

            await _context.ProfessionalAreas.AddAsync(resultProf);
            await _context.SaveChangesAsync();

            return resultProf.ProfessionalAreaGuid;
        }

        /// <summary>
        /// Изменяет название професии
        /// </summary>
        /// <param name="id">Id професии</param>
        /// <param name="newModel">Новая модель, из которой будут браться данные</param>
        /// <returns></returns>
        public async Task EditAsync(Guid id, ProfessionalAreaModel newModel)
        {
            if (newModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.ProfessionalAreaGuid == id);
            if (resultProf == null)
            {
                throw new NullReferenceException($"Професии с  таким id:{id} не существует.");
            }

            resultProf.Name = newModel.Name;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет професию по id
        /// </summary>
        /// <param name="id">Id професии</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.ProfessionalAreaGuid == id);
            if (resultProf == null)
            {
                throw new NullReferenceException($"Професии с  таким id:{id} не существует.");
            }
            _context.ProfessionalAreas.Remove(resultProf);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список професий
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProfessionalArea>> GetAsync()
        {
            return await _context.ProfessionalAreas
                .Include(x=>x.Resumes)
                .ToListAsync();
        }
    }
}
