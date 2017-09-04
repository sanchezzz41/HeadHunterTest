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
    /// Класс реализующий IResumeService
    /// </summary>
    public class ResumeService : IResumeService
    {
        /// <summary>
        /// Список резюме
        /// </summary>
        public List<Resume> Resumes => _context.Resumes
            .Include(x => x.JobSeeker)
            .Include(x => x.ResumeInCity)
            .Include(x => x.ProfessionalArea)
            .ToList();

        private readonly DatabaseContext _context;

        public ResumeService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новое резюме соискателя
        /// </summary>
        /// <param name="idJobSeeker">Id соискателя, которому будет добавлено резюме</param>
        /// <param name="model">Модель содержащая данные для резюме</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idJobSeeker, ResumeModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultJobSeeker = await _context.JobSeekers.SingleOrDefaultAsync(x => x.Id == idJobSeeker);
            if (resultJobSeeker == null)
            {
                throw new NullReferenceException($"Соискателя с id: {idJobSeeker} не существует.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == model.CityId);
            if(resultCity==null)
            {
                throw new NullReferenceException($"Города с id: {model.CityId} не существует.");
            }

            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.Id == model.ProfAreaId);
            if (resultProf == null)
            {
                throw new NullReferenceException($"Професии с id: {model.ProfAreaId} не существует.");
            }

            var resultResume = new Resume(idJobSeeker, resultCity.Id, resultProf.Id, model.Salary,
                model.DesiredPosition);

            await _context.Resumes.AddAsync(resultResume);
            await _context.SaveChangesAsync();

            return resultResume.Id;
        }

        /// <summary>
        /// Изменяет резюме
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо изменить</param>
        /// <param name="model">Модель для изменения резюме</param>
        /// <returns></returns>
        public async Task EditAsync(Guid idResume, ResumeModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultResume = await _context.Resumes.SingleOrDefaultAsync(x => x.Id == idResume);
            if (resultResume == null)
            {
                throw new NullReferenceException($"Резюме с id: {idResume} не существует.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == model.CityId);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Города с id: {model.CityId} не существует.");
            }

            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.Id == model.ProfAreaId);
            if (resultProf == null)
            {
                throw new NullReferenceException($"Професии с id: {model.ProfAreaId} не существует.");
            }

            resultResume.CityId = model.CityId;
            resultResume.ProfAreaId = model.ProfAreaId;
            resultResume.DesiredPosition = model.DesiredPosition;
            resultResume.Salary = model.Salary;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет резюме по Id
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо удалить</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idResume)
        {
            var resultResume = await _context.Resumes.SingleOrDefaultAsync(x => x.Id == idResume);
            if (resultResume == null)
            {
                throw new NullReferenceException($"Резюме с id: {idResume} не существует.");
            }

            _context.Resumes.Remove(resultResume);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список резюме
        /// </summary>
        /// <returns></returns>
        public async Task<List<Resume>> GetAsync()
        {
            return await _context.Resumes
                .Include(x=>x.JobSeeker)
                .Include(x=>x.ResumeInCity)
                .Include(x=>x.ProfessionalArea)
                .ToListAsync();
        }
    }
}

