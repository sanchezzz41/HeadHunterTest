using System;
using System.Collections.Generic;
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
    /// Контроллер для работы с профессиями(доступен только админу)
    /// </summary>
    [Authorize(Roles = nameof(RolesOption.Admin))]
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
        public async Task<object> Add([FromBody] ProfessionalAreaModel model)
        {
            return await _professionalAreaService.AddAsync(model);
        }

        //Удаление професии
        [HttpDelete]
        public async Task Delete([FromQuery]Guid idProf)
        {
            await _professionalAreaService.DeleteAsync(idProf);
        }

        //Изменение професии
        [HttpPut]
        public async Task Edit([FromBody] ProfessionalAreaModel model, [FromQuery]Guid idProf)
        {
            await _professionalAreaService.EditAsync(idProf, model);
        }

        //Возвращение всех професий
        [HttpGet]
        public async Task<object> Get()
        {
            var resultProfs = await _professionalAreaService.GetAsync();
            return resultProfs.Select(x => x?.ProfView());
        }
    }
}
