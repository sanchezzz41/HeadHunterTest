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

namespace HeadHunterTest.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с резюме
    /// </summary>
    [Authorize(Roles = nameof(RolesOptions.JobSeeker))]
    [Route("Resumes")]
    public class ResumesController : Controller
    {
        private readonly IResumeService _resumeService;

        public ResumesController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        //Добавляет резюме
        [HttpPost]
        public async Task<object> Add([FromBody] ResumeModel resumeModel,[FromQuery] Guid idJobSeeker)
        {
            return await _resumeService.AddAsync(idJobSeeker, resumeModel);
        }

        //Изменяет резюме
        [HttpPut]
        public async Task Edit([FromBody] ResumeModel resumeModel, [FromQuery] Guid idResume)
        {
            await _resumeService.EditAsync(idResume, resumeModel);
        }

        //Удаляет резюме
        [HttpDelete]
        public async Task Delete([FromQuery] Guid idResume)
        {
            await _resumeService.DeleteAsync(idResume);
        }

        //Возвращает список резюме
        [HttpGet]
        public async Task<object> Get()
        {
            var resumeList = await _resumeService.GetAsync();
            return resumeList.Select(x => x.ResumeView());
        }
    }
}
