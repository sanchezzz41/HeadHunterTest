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
        public async Task<Guid> AddAsync(VacancyModel vacancyModel)
        {
            if (vacancyModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == vacancyModel.CityGuid);
          
            var resultEmpoyer = await _context.Employers.SingleAsync(x => x.UserGuid == vacancyModel.EmployerId);

            var resultEmp = await _context.Employments.SingleAsync(x => x.EmploymentId == vacancyModel.EmploymentId);

            var resultProfArea = await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == vacancyModel.ProfAreaGuid);

            var resultVacancy = new Vacancy(resultEmpoyer.UserGuid, resultCity.CityGuid,
                resultProfArea.ProfessionalAreaGuid, resultEmp.EmploymentId, vacancyModel.Salary, vacancyModel.Position,
                vacancyModel.WorkExpirience, vacancyModel.Description, vacancyModel.Phone);
             _context.Vacancies.Add(resultVacancy);
            await _context.SaveChangesAsync();

            return resultVacancy.VacancyGuid;
        }

        /// <summary>
        /// Изменяет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditAsync(Guid idVacancy, VacancyModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultVacancy = await _context.Vacancies.SingleAsync(x => x.VacancyGuid == idVacancy);

            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == model.CityGuid);

            var resultEmp = await _context.Employments.SingleAsync(x => x.EmploymentId == model.EmploymentId);

            var resultProfArea = await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == model.ProfAreaGuid);

            resultVacancy.CityGuid = resultCity.CityGuid;
            resultVacancy.Emp = resultCity.CityGuid;
            resultVacancy.CityGuid = resultCity.CityGuid;
            resultVacancy.CityGuid = resultCity.CityGuid;
            resultVacancy.Description = model.Description;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет вакансию по Id
        /// </summary>
        /// <param name="idVacancy"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idVacancy)
        {
            var resultVacancy = await _context.Vacancies.SingleOrDefaultAsync(x => x.VacancyGuid == idVacancy);
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

            var listResVac = await _context.Notes
                .Include(x => x.Resume)
                .ToListAsync();

            var listVacancies = listResVac
                .Where(x => x.VacancyGuid == idVacancy)
                .Select(x => x.Resume)
                .ToList();

            return listVacancies;
        }
    }
}
