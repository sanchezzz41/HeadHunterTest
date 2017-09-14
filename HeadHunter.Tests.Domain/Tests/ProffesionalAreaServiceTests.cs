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
    /// Тесты для проф областай
    /// </summary>
    [TestFixture]
    class ProffesionalAreaServiceTests
    {
        //Тестируемый сервис
        private IProfessionalAreaService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<ProfessionalArea> _listTest;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().CreateProfArea();
            _listTest = await _context.ProfessionalAreas.ToListAsync();
            _listTest = _listTest.OrderBy(x => x.Name).ToList();

            //Services
            _service = new ProfessionalAreaService(_context);

        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().Dispose();
        }


        /// <summary>
        ///  Добавляет проф область (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddProf_Success()
        {
            //Arrange
            var profArea = new ProfessionalAreaModel {Name = "Полицейский"};
            //act
            var id = await _service.AddAsync(profArea);
            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.Id == id);
            //assert
            Assert.AreEqual(profArea.Name, resultProf.Name);
        }

        /// <summary>
        ///  Изменяет проф область (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task EditProf_Success()
        {
            //Arrange
            var newProfArea = new ProfessionalAreaModel {Name = "Полицейский"};
            var profArea = await _context.ProfessionalAreas.FirstAsync();
            //act
            await _service.EditAsync(profArea.Id, newProfArea);
            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.Name == newProfArea.Name);
            //assert
            Assert.AreEqual(newProfArea.Name, resultProf.Name);
        }

        /// <summary>
        ///  Удаляет проф область (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteProf_Success()
        {
            //Arrange
            var profArea = await _context.ProfessionalAreas.FirstAsync();
            //act
            await _service.DeleteAsync(profArea.Id);
            var resultProf = await _context.ProfessionalAreas.SingleOrDefaultAsync(x => x.Id == profArea.Id);
            //assert
            Assert.IsNull(resultProf);
        }

        /// <summary>
        ///  Возвращает все проф области (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetProf_Success()
        {
            //Arrange

            //act
            var list = await _service.GetAsync();
            list = list.OrderBy(x => x.Name).ToList();
            //assert
            CollectionAssert.AreEqual(_listTest, list);
        }
    }
}
