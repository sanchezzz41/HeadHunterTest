using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.ProfAreas;
using HeadHunterTest.Domain.ProfAreas.Models;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeadHunterTest.Web.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с профессиями(доступен только админу)
    /// </summary>
    [Authorize(Roles = nameof(RolesOptions.Admin))]
    [Route("Admin/Profs")]
    public class ProfessionalAreasController : Controller
    {
        private readonly IProfessionalAreaService _professionalAreaService;

        public ProfessionalAreasController(IProfessionalAreaService professionalAreaService)
        {
            _professionalAreaService = professionalAreaService;
        }

        //Добавление професии
        [HttpPost]
        public async Task<object> Add([FromBody] ProfessionalAreaInfo profModel)
        {
            return await _professionalAreaService.AddAsync(profModel);
        }

        //Удаление професии
        [HttpDelete]
        public async Task Delete([FromQuery]Guid idProf)
        {
            await _professionalAreaService.DeleteAsync(idProf);
        }

        //Изменение професии
        [HttpPut]
        public async Task Edit([FromBody] ProfessionalAreaInfo profModel, [FromQuery]Guid idProf)
        {
            await _professionalAreaService.EditAsync(idProf, profModel);
        }

        //Возвращение всех професий
        [HttpGet]
        public async Task<object> Get()
        {
            var profList = await _professionalAreaService.GetAsync();
            return profList.Select(x => x?.ProfView());
        }
    }
}
