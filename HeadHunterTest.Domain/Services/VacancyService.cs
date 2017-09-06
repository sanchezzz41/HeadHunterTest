using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Services
{
    /// <summary>
    /// Класс реализующий IVacancyService
    /// </summary>
    public class VacancyService: IVacancyService
    {
        /// <summary>
        /// Список вакансий
        /// </summary>
        public List<Vacancy> Vacancies => _context.Vacancies
            .Include(x => x.VacanciesInCity)
            .Include(x => x.Employer)
            .Include(x => x.ResumeVacancies)
            .ToList();

        private readonly DatabaseContext _context;

        public VacancyService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет вакансию
        /// </summary>
        /// <param name="vacancyModel"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idEmployer, VacancyModel vacancyModel)
        {
            if (vacancyModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == vacancyModel.CityId);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Города с Id:{vacancyModel.CityId} нету.");
            }

            var resultEmp = await _context.Employers.SingleOrDefaultAsync(x => x.Id == idEmployer);
            if (resultEmp == null)
            {
                throw new NullReferenceException($"Работодателя с Id:{idEmployer} нету.");
            }

            var resultVacancy = new Vacancy(idEmployer, vacancyModel.CityId, vacancyModel.Description);

            await _context.Vacancies.AddAsync(resultVacancy);
            await _context.SaveChangesAsync();

            return resultVacancy.Id;
        }

        /// <summary>
        /// Изменяет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid idVacancy, VacancyModel newModel)
        {
            if (newModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultVacancy = await _context.Vacancies.SingleOrDefaultAsync(x => x.Id == idVacancy);
            if (resultVacancy == null)
            {
                throw new NullReferenceException($"Вакансии с Id:{idVacancy} нету.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == newModel.CityId);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Города с Id:{newModel.CityId} нету.");
            }

            resultVacancy.CityId = resultCity.Id;
            resultVacancy.Description = newModel.Description;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idVacancy)
        {
            var resultVacancy = await _context.Vacancies.SingleOrDefaultAsync(x => x.Id == idVacancy);
            if (resultVacancy == null)
            {
                throw new NullReferenceException($"Вакансии с Id:{idVacancy} нету.");
            }

            _context.Vacancies.Remove(resultVacancy);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список вакансий
        /// </summary>
        /// <returns></returns>
        public async Task<List<Vacancy>> GetAsync()
        {
            return await _context.Vacancies
                .Include(x => x.VacanciesInCity)
                .Include(x => x.Employer)
                .Include(x=>x.ResumeVacancies)
                .ToListAsync(); 
        }

        /// <summary>
        /// Возвращает список резюме, которые закреплены за вакансией
        /// </summary>
        /// <param name="idVacancy">Id вакаснии, список резюме которой надо получить</param>
        /// <returns></returns>
        public async Task<List<Resume>> GetAttachmentsResumes(Guid idVacancy)
        {

            var listResVac = await _context.ResumeVacancies
                .Include(x => x.Resume)
                .ToListAsync();

            var listVacancies = listResVac
                .Where(x => x.VacancyId == idVacancy)
                .Select(x => x.Resume)
                .ToList();

            return listVacancies;
        }
    }
}
