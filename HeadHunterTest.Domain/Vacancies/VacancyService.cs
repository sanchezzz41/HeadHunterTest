﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes;
using HeadHunterTest.Domain.Notes.Models;
using HeadHunterTest.Domain.Vacancies.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Vacancies
{
    /// <summary>
    /// Класс реализующий IVacancyService
    /// </summary>
    public class VacancyService : IVacancyService
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
        private readonly INoteService _noteService;

        public VacancyService(DatabaseContext context, INoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        /// <summary>
        /// Добавляет вакансию
        /// </summary>
        /// <param name="employerGuid"></param>
        /// <param name="vacancyModel"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid employerGuid, VacancyInfo vacancyModel)
        {
            if (vacancyModel == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == vacancyModel.CityGuid);

            var resultEmpoyer = await _context.Employers.SingleAsync(x => x.UserGuid == employerGuid);

            var resultEmp = await _context.Employments.SingleAsync(x => x.EmploymentId == vacancyModel.EmploymentId);

            var resultProfArea =
                await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == vacancyModel.ProfAreaGuid);

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
        public async Task EditAsync(Guid idVacancy, VacancyInfo model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывет на null.");
            }

            var resultVacancy = await _context.Vacancies.SingleAsync(x => x.VacancyGuid == idVacancy);

            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == model.CityGuid);

            var resultEmp = await _context.Employments.SingleAsync(x => x.EmploymentId == model.EmploymentId);

            var resultProfArea =
                await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == model.ProfAreaGuid);

            resultVacancy.CityGuid = resultCity.CityGuid;
            resultVacancy.EmploymentId =resultEmp.EmploymentId;
            resultVacancy.ProfAreaGuid = resultProfArea.ProfessionalAreaGuid;
            resultVacancy.Description = model.Description;
            resultVacancy.Salary = model.Salary;
            resultVacancy.WorkExpirience = model.WorkExpirience;
            resultVacancy.Phone = model.Phone;

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
        public IQueryable<Vacancy> Get()
        {
            return _context.Vacancies;
        }

        public async Task AttachVacancy(NoteInfo model)
        {
            await _noteService.AttachResumeToVacancy(model, true);
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
