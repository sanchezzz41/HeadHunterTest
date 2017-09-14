using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadHunter.Tests.Domain.Factory;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using HeadHunterTest.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HeadHunter.Tests.Domain.Tests
{
    /// <summary>
    /// Класс для проверки резюме
    /// </summary>
    [TestFixture]
    public class ResumeServiceTests
    {
        //Тестируемый сервис
        private IResumeService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<Resume> _listTest;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<CityDataFactory>().CreateCities();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().CreateProfArea();
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            await TestInitializer.Provider.GetService<ResumeDataFactory>().CreateResume();
            await TestInitializer.Provider.GetService<VacancyDataFactory>().CreateVacancy();
            _listTest = await _context.Resumes.ToListAsync();
            _listTest = _listTest.OrderBy(x => x.Salary).ToList();

            //Services
            _service = new ResumeService(_context);

        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<CityDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ResumeDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<VacancyDataFactory>().Dispose();

        }

        /// <summary>
        ///  Добавляет резюме (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddResume_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            var prof = await _context.ProfessionalAreas.FirstAsync();
            var jobSeeker = await _context.JobSeekers.FirstAsync();
            var expectedResume = new ResumeModel
            {
                DesiredPosition = "Начальник",
                Salary = 1111,
                ProfAreaId = prof.Id,
                CityId = city.Id

            };

            //act
            var id = await _service.AddAsync(jobSeeker.Id, expectedResume);
            var resultResume = await _context.Resumes.SingleOrDefaultAsync(x => x.Id == id);
          
            //assert
            Assert.AreEqual(expectedResume.Salary, resultResume.Salary);
            Assert.AreEqual(expectedResume.DesiredPosition, resultResume.DesiredPosition);
            Assert.AreEqual(expectedResume.CityId, resultResume.CityId);
            Assert.AreEqual(expectedResume.ProfAreaId, resultResume.ProfAreaId);
        }


        /// <summary>
        ///  Прикрепляет резюме к вакансии(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AttachmentResumeToVacancy_Success()
        {
            //Arrange
            var resume = await _context.Resumes.FirstAsync();
            var vacancy = await _context.Vacancies.FirstAsync();

            //act
            await _service.AffixResumeToVacancy(resume.Id, vacancy.Id);
            var resultVacancyResume = resume.ResumeVacancies.SingleOrDefault(x => x.ResumeId == resume.Id);
            
            //assert
            Assert.IsNotNull(resultVacancyResume);
        }

        /// <summary>
        ///  Удаляет резюме (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteResume_Success()
        {
            //Arrange
            var resume = await _context.Resumes.FirstAsync();

            //act
            await _service.DeleteAsync(resume.Id);
            var resultResume = await _context.Resumes.SingleOrDefaultAsync(x => x.Id == resume.Id);
           
            //assert
            Assert.IsNull(resultResume);
        }

        /// <summary>
        ///  Получает список резюме (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetResume_Success()
        {
            //Arrange

            //act
            var list = await _service.GetAsync();
            list = list.OrderBy(x => x.Salary).ToList();
            //assert
            CollectionAssert.AreEqual(_listTest, list);
        }
    }
}
