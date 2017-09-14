using System.Collections.Generic;
using System.Linq;
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
    /// Тесты для ICityService
    /// </summary>
    [TestFixture]
    public class CityServiceTests
    {

        //Тестируемый сервис
        private ICityService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<City> _listTest;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<CityDataFactory>().CreateCities();
            _listTest = await _context.Cities.ToListAsync();
            _listTest = _listTest.OrderBy(x => x.Name).ToList();

            //Services
            _service = new CityService(_context);

        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<CityDataFactory>().Dispose();
        }

        /// <summary>
        ///  Добавляет город (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddCity_Success()
        {
            //Arrange
            var city = new CityModel {Name = "Калуга"};
            //act
            var id = await _service.AddAsync(city);
            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == id);
            //assert
            Assert.AreEqual(city.Name, resultCity.Name);
        }

        /// <summary>
        ///  Возвращение все города(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCity_Success()
        {
            //Arrange

            //act
            var resultList = await _service.GetAsync();
            resultList = resultList.OrderBy(x => x.Name).ToList();
            //assert
            CollectionAssert.AreEqual(_listTest, resultList);
        }

        /// <summary>
        ///  Измеяет город(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task EditCity_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            var newCity = new CityModel {Name = "Калуга"};
            //act
            await _service.EditAsync(city.Id, newCity);
            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == city.Id);
            //assert
            Assert.AreEqual(newCity.Name, resultCity.Name);
        }

        /// <summary>
        ///  Удаляет город(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteCity_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            //act
            await _service.DeleteAsync(city.Id);
            var resultCity = await _context.Cities.SingleOrDefaultAsync(x => x.Id == city.Id);
            //assert
            Assert.IsNull(resultCity);
        }
    }
}
