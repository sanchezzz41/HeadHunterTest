﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes;
using HeadHunterTest.Domain.Notes.Models;
using HeadHunterTest.Domain.Resumes.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Resumes
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
            .Include(x => x.ResumeVacancies)
            .ToList();

        private readonly DatabaseContext _context;
        private readonly INoteService _noteService;

        public ResumeService(DatabaseContext context, INoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        /// <summary>
        /// Добавляет новое резюме соискателя
        /// </summary>
        /// <param name="idJobSeeker">Id соискателя, которому будет добавлено резюме</param>
        /// <param name="model">Модель содержащая данные для резюме</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(Guid idJobSeeker, ResumeInfo model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultJobSeeker = await _context.JobSeekers.SingleAsync(x => x.UserGuid == idJobSeeker);

            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == model.CityGuid);
            

            var resultProf = await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == model.ProfAreaGuid);
       
            var resultResume = new Resume(resultJobSeeker.UserGuid, resultCity.CityGuid, resultProf.ProfessionalAreaGuid, model.EmploymentId,
                model.Salary, model.Position, model.WorkExpirience, model.Description);

            await _context.Resumes.AddAsync(resultResume);
            await _context.SaveChangesAsync();

            return resultResume.ResumeGuid;
        }

        /// <summary>
        /// Изменяет резюме
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо изменить</param>
        /// <param name="model">Модель для изменения резюме</param>
        /// <returns></returns>
        public async Task EditAsync(Guid idResume, ResumeInfo model)
        {
            if (model == null)
            {
                throw new NullReferenceException($"Ссылка на модель указывает на null.");
            }

            var resultResume = await _context.Resumes.SingleAsync(x => x.ResumeGuid == idResume);
          
            var resultCity = await _context.Cities.SingleAsync(x => x.CityGuid == model.CityGuid);

            var resultProf = await _context.ProfessionalAreas.SingleAsync(x => x.ProfessionalAreaGuid == model.ProfAreaGuid);

            resultResume.CityGuid = resultCity.CityGuid;
            resultResume.ProfAreaGuid = resultProf.ProfessionalAreaGuid;
            resultResume.EmploymentId = model.EmploymentId;
            resultResume.Salary = model.Salary;
            resultResume.Description = model.Description;
            resultResume.Position = model.Position;
            resultResume.WorkExpirience = model.WorkExpirience;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет резюме по Id
        /// </summary>
        /// <param name="idResume">Id резюме, которое надо удалить</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idResume)
        {
            var resultResume = await _context.Resumes.SingleOrDefaultAsync(x => x.ResumeGuid == idResume);
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
        public IQueryable<Resume> Get()
        {
            return _context.Resumes;
        }

        /// <summary>
        /// Прикрепляет резюме к вакансии
        /// </summary>
        /// <param name="idResume">Id резюме</param>
        /// <param name="idVacancy">Id вакансии</param>
        /// <returns></returns>
        public async Task AttachResume(Guid idResume, Guid idVacancy)
        {
            var noteInfo = new NoteInfo
            {
                ResumeGuid = idResume,
                VacancyGuid = idVacancy
            };
            await _noteService.AttachResumeToVacancy(noteInfo, false);
        }

        /// <summary>
        /// Возвращает список вакансий, которые прикриплены к резюме 
        /// </summary>
        /// <param name="idResume">Id резюме, вакансии которого будут возвращены</param>
        /// <returns></returns>
        public async Task<List<Vacancy>> GetAttachmentsVacancies(Guid idResume)
        {
            var listResVac = await _context.Notes
                .Include(x => x.Vacancy)
                .ToListAsync();

            var listVacancies = listResVac
                .Where(x => x.ResumeGuid == idResume)
                .Select(x=>x.Vacancy)
                .ToList();

            return listVacancies;
        }
    }
}