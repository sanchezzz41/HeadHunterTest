using System;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeadHunterTest.Web.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с городами(доступен только админу)
    /// </summary>
    [Authorize(Roles = nameof(RolesOptions.Admin))]
    [Route("Admin/Cities")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        //Добавление города
        [HttpPost]
        public async Task<object> Add([FromBody] CityModel model)
        {
            return await _cityService.AddAsync(model);
        }

        //Удаление города
        [HttpDelete]
        public async Task Delete([FromQuery] Guid idCity)
        {
            await _cityService.DeleteAsync(idCity);
        }

        //Изменение города
        [HttpPut]
        public async Task Edit([FromBody] CityModel model, [FromQuery] Guid idCity)
        {
            await _cityService.EditAsync(idCity, model);
        }

        //Возвращение города
        [HttpGet]
        public async Task<object> Get()
        {
            var resultCities = await _cityService.GetAsync();
            return resultCities.Select(x => x?.CityView());
        }
    }
}
