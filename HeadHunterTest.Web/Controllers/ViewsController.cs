using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Interfaces;
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
    [Authorize]
    public class ViewsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IResumeService _resumeService;
        private readonly IVacancyService _vacancyService;
        private readonly ICityService _cityService;
        private readonly IProfessionalAreaService _professionalAreaService;

        public ViewsController(IUserService userService, IResumeService resumeService, IVacancyService vacancyService,
            ICityService cityService, IProfessionalAreaService professionalAreaService)
        {
            _userService = userService;
            _resumeService = resumeService;
            _vacancyService = vacancyService;
            _cityService = cityService;
            _professionalAreaService = professionalAreaService;
        }

        //Возвращает инфо о пользователях
        [HttpGet("Users")]
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

        //Возвращает инфо о резюме
        [HttpGet("Resumes")]
        public async Task<object> GetResumes()
        {
            var list = await _resumeService.GetAsync();
            return list.Select(x => x?.ResumeView());
        }

        //Возвращает инфо о вакансиях
        [HttpGet("Vacancies")]
        public async Task<object> GetVacancies()
        {
            var list = await _vacancyService.GetAsync();
            return list.Select(x => x?.VacancyView());
        }
    }
}
