using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HeadHunterTest.Domain.Notes
{
    public class NoteService : INoteService
    {
        private readonly DatabaseContext _context;

        public NoteService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AttachResumeToVacancy(NoteInfo info, bool isEmployer)
        {
            var resultNote = await GetNote(info);
            resultNote.IsEmployer = isEmployer;
            _context.Notes.Add(resultNote);
            await _context.SaveChangesAsync();
        }

        public async Task AttachVacancyToResume(NoteInfo info)
        {
            var resultNote = await GetNote(info);
            resultNote.IsEmployer = true;
            _context.Notes.Add(resultNote);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Resume>> SearchResume(SearchResumeInfo model)
        {
            if (model == null)
                throw new ArgumentNullException($"Модель равняется null.");

            var resultList = _context.Resumes.AsNoTracking();
            var listQuaryble = new List<IQueryable<Resume>>();
            var resultWords = GetNormilizeStrings(model.SearchString);
            resultWords.Add(model.SearchString.ToLower());
            foreach (var resultWord in resultWords)
            {
                listQuaryble.Add(resultList.Where(x => x.Position.ToLower().Contains(resultWord)
                                                       || x.Description.ToLower().Contains(resultWord)
                                                       || x.Salary.ToString().Contains(resultWord)));
            }
            var result = listQuaryble
                .SelectMany(x => x.ToList())
                .GroupBy(x => x.ResumeGuid)
                .OrderByDescending(x => x.Count())
                .SelectMany(a => a.Select(b => b))
                .Distinct((x, a) => x.ResumeGuid == a.ResumeGuid);

            if (model.EmploymentOption != null)
                result = result.Where(x => x.EmploymentId == model.EmploymentOption);
            if (model.CityGuid != null)
                result = result.Where(x => x.CityGuid == model.CityGuid);
            if (model.CreatedTime != null)
                result = result.Where(x => x.DateResume >= model.CreatedTime);
            if (model.Salary != null)
                result = result.Where(x => x.Salary >= model.Salary);
            if (model.WorkExpirience != null)
                result = result.Where(x => x.WorkExpirience >= model.WorkExpirience);

            return result.ToList();
        }

        public async Task<IEnumerable<Vacancy>> SearchVacancy(SearchResumeInfo model)
        {
            if (model == null)
                throw new ArgumentNullException($"Модель равняется null.");

            var resultList = _context.Vacancies.AsNoTracking();
            var listQuaryble = new List<IQueryable<Vacancy>>();
            var resultWords = GetNormilizeStrings(model.SearchString);
            resultWords.Add(model.SearchString.ToLower());
            foreach (var resultWord in resultWords)
            {
                listQuaryble.Add(resultList.Where(x => x.Position.ToLower().Contains(resultWord)
                                                       || x.Description.ToLower().Contains(resultWord)
                                                       || x.Phone.ToLower().Contains(resultWord)
                                                       || x.Salary.ToString().Contains(resultWord)));
            }
            var result = listQuaryble
                .SelectMany(x => x.ToList())
                .GroupBy(x => x.VacancyGuid)
                .OrderByDescending(x => x.Count())
                .SelectMany(a => a.Select(b => b))
                .Distinct((x, a) => x.VacancyGuid == a.VacancyGuid);
            if (model.EmploymentOption != null)
                result = result.Where(x => x.EmploymentId == model.EmploymentOption);
            if (model.CityGuid != null)
                result = result.Where(x => x.CityGuid == model.CityGuid);
            if (model.CreatedTime != null)
                result = result.Where(x => x.DateVacancy >= model.CreatedTime);
            if (model.Salary != null)
                result = result.Where(x => x.Salary >= model.Salary);
            if (model.WorkExpirience != null)
                result = result.Where(x => x.WorkExpirience >= model.WorkExpirience);

            return result.ToList();
        }

        private List<string> GetNormilizeStrings(string searchString)
        {
            return searchString.Split(new char[] {' ', ',', '.', '-'})
                .Where(x => x.Length > 3)
                .Select(x => x.ToLower())
                .Distinct()
                .Where(x => x != "")
                .ToList();

        }

        private async Task<Note> GetNote(NoteInfo info)
        {
            if (info == null)
                throw new ArgumentNullException($"Ссылка на модель указывает на null.");
            var resultResume = await _context.Resumes.SingleAsync(x => x.ResumeGuid == info.ResumeGuid);
            var resultVacancy = await _context.Vacancies.SingleAsync(x => x.VacancyGuid == info.VacancyGuid);

            return new Note(resultResume.ResumeGuid, resultVacancy.VacancyGuid, false);
        }
    }
}
