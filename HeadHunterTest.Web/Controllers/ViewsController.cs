using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Cities;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes;
using HeadHunterTest.Domain.Notes.Models;
using HeadHunterTest.Domain.ProfAreas;
using HeadHunterTest.Domain.Resumes;
using HeadHunterTest.Domain.Resumes.Models;
using HeadHunterTest.Domain.Users;
using HeadHunterTest.Domain.Vacancies;
using HeadHunterTest.Domain.Vacancies.Models;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeadHunterTest.Web.Controllers
{
    /// <summary>
    /// Контроллер который отображает данные о всех таблицах бд
    /// </summary>
    [Route("Views")]
    public class ViewsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IResumeService _resumeService;
        private readonly IVacancyService _vacancyService;
        private readonly ICityService _cityService;
        private readonly IProfessionalAreaService _professionalAreaService;
        private readonly INoteService _noteService;

        public ViewsController(IUserService userService, IResumeService resumeService, IVacancyService vacancyService,
            ICityService cityService, IProfessionalAreaService professionalAreaService, INoteService noteService)
        {
            _userService = userService;
            _resumeService = resumeService;
            _vacancyService = vacancyService;
            _cityService = cityService;
            _professionalAreaService = professionalAreaService;
            _noteService = noteService;
        }

        //Возвращает инфо о пользователях
        [HttpGet("Users")]
        [Authorize]
        public async Task<object> GetUsers()
        {
            var list = await _userService.GetAsync();
            return list.Select(x => x?.UserView());
        }

        //Возвращает инфо о города
        [HttpGet("Cities")]
        public async Task<object> GetCities()
        {
            var list = await _cityService.GetAsync();
            return list.Select(x => x?.CityView());
        }

        //Возвращает инфо о професиях
        [HttpGet("Profs")]
        public async Task<object> GetProfs()
        {
            var list = await _professionalAreaService.GetAsync();
            return list.Select(x => x?.ProfView());
        }

        //Возвращает список резюме
        [HttpGet("Resumes")]
        [Authorize]
        public async Task<IEnumerable<ResumeModel>> GetResume()
        {
            var resumeList = _resumeService.Get();
            return await resumeList.GetCollection(x => new ResumeModel(x));
        }

        //Возвращает все вакансии
        [HttpGet("Vacancies")]
        [Authorize]
        public async Task<object> GetVacancy()
        {
            var list =  _vacancyService.Get();
            return await list.GetCollection(x => new VacancyModel(x));
        }

        [HttpGet("SearchResume")]
        public async Task<object> SearchResume([FromQuery]string searchString,[FromQuery] EmploymentOption? empOpt,
            [FromQuery] double? workExpirience,[FromQuery] decimal? salary,[FromQuery] Guid? cityGuid,
            [FromQuery] Guid? proffesionAreaGuid,[FromQuery] DateTimeOffset? createdTime)
        {
            var model = new SearchResumeInfo
            {
                CityGuid = cityGuid,
                CreatedTime = createdTime,
                EmploymentOption = empOpt,
                ProfAreaGuid = proffesionAreaGuid,
                Salary = salary,
                SearchString = searchString,
                WorkExpirience = workExpirience
            };
            var list = await _noteService.SearchResume(model);
            return await list.GetCollection(x => new ResumeModel(x));
        }

        [HttpGet("SearchVacancy")]
        public async Task<object> SearchVacancy([FromQuery]string searchString, [FromQuery] EmploymentOption? empOpt,
            [FromQuery] double? workExpirience, [FromQuery] decimal? salary, [FromQuery] Guid? cityGuid,
            [FromQuery] Guid? proffesionAreaGuid, [FromQuery] DateTimeOffset? createdTime)
        {
            var model = new SearchResumeInfo
            {
                CityGuid = cityGuid,
                CreatedTime = createdTime,
                EmploymentOption = empOpt,
                ProfAreaGuid = proffesionAreaGuid,
                Salary = salary,
                SearchString = searchString,
                WorkExpirience = workExpirience
            };
            var list = await _noteService.SearchVacancy(model);
            return await list.GetCollection(x => new VacancyModel(x));
        }
    }
}
