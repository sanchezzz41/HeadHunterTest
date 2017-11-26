using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Notes.Models;

namespace HeadHunterTest.Domain.Notes
{
    public interface INoteService
    {
        /// <summary>
        /// Прикрепляет резюме к вакансии(должен делать соискатель)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isEmployer"></param>
        /// <returns></returns>
        Task AttachResumeToVacancy(NoteInfo info, bool isEmployer);

        /// <summary>
        /// Прикрепляет вакансию к резюме(должен делать работодатель)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task AttachVacancyToResume(NoteInfo info);

        Task<IEnumerable<Resume>> SearchResume(SearchResumeInfo info);

        Task<IEnumerable<Vacancy>> SearchVacancy(SearchResumeInfo info);
    }
}
