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
    /// Класс для проверки для вакансий
    /// </summary>
    [TestFixture]
    public class VacancyServiceTests
    {
        //Тестируемый сервис
        private IVacancyService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<Vacancy> _listTest;


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
            _listTest = await _context.Vacancies.ToListAsync();
            _listTest = _listTest.OrderBy(x => x.Description).ToList();

            //Services
            _service = new VacancyService(_context);

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
        ///  Добавляет вакансию(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddVacancy_Success()
        {
            //Arrange
            var emp = await _context.Employers.FirstAsync();
            var city = await _context.Cities.FirstAsync();
            var actualVac = new VacancyModel
            {
             CityId    = city.Id,
             Description = "какое то описание"
            };
            //act
            var id = await _service.AddAsync(emp.Id, actualVac);
            var resultVacancy = await _context.Vacancies.SingleOrDefaultAsync(x => x.Id == id);

            //assert
            Assert.AreEqual(actualVac.Description, resultVacancy.Description);
        }

        /// <summary>
        ///  Удаляет вакансию(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteVacancy_Success()
        {
            //Arrange
            var vac = await _context.Vacancies.FirstAsync();
            //act
            await _service.DeleteAsync(vac.Id);
            var actualVac = await _context.Vacancies.SingleOrDefaultAsync(x => x.Id == vac.Id);

            //assert
            Assert.IsNull(actualVac);
        }

        /// <summary>
        ///  Возвращает вакансии(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetVacancy_Success()
        {
            //Arrange
          
            //act
            var resultVacancies = await _service.GetAsync();
            resultVacancies = resultVacancies.OrderBy(x => x.Description).ToList();

            //assert
            CollectionAssert.AreEqual(_listTest,resultVacancies);
        }
    }
}
