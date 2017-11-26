using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Vacancies;
using HeadHunterTest.Domain.Vacancies.Models;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeadHunterTest.Web.Controllers
{
    /// <summary>
    /// Контроллер д
    /// </summary>
    [Route("Vacancies")]
    [Authorize(Roles = nameof(RolesOptions.Employer))]
    public class VacanciesController : Controller
    {
        private readonly IVacancyService _vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        //Добавляет вакансию пользователю
        [HttpPost]
        public async Task<object> Add([FromBody] VacancyInfo vacModel,[FromQuery] Guid idEmployer)
        {
            return await _vacancyService.AddAsync(idEmployer,vacModel);
        }

        //Изменяет вакансию
        [HttpPut]
        public async Task Edit([FromBody] VacancyInfo vacModel,[FromQuery] Guid idVacancy)
        {
            await _vacancyService.EditAsync(idVacancy, vacModel);
        }

        //Изменяет вакансию
        [HttpDelete]
        public async Task Delete([FromQuery] Guid idVacancy)
        {
            await _vacancyService.DeleteAsync(idVacancy);
        }

        //Возвращает резюме для данной вакансии
        [HttpGet("Resumes")]
        public async Task<object> GetResumeByIdVac([FromQuery] Guid idVac)
        {
            var list = await _vacancyService.GetAttachmentsResumes(idVac);
            return list.Select(x => x?.ResumeView());
        }
    }
}
