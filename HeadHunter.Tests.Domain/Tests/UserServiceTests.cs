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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace HeadHunter.Tests.Domain.Tests
{
    /// <summary>
    /// Тесты для пользователей
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        //Тестируемый сервис
        private IUserService _service;

        //Контекст бд
        private DatabaseContext _context;

        //Хранилще для проверки
        private List<User> _listTest;


        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<CityDataFactory>().CreateCities();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().CreateProfArea();
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _listTest = await _context.Users.ToListAsync();
            _listTest = _listTest.OrderBy(x => x.Email).ToList();

            //Services
            var passwordHasher = await GetPassworhHasher();
            _service = new UserService(_context, passwordHasher);

        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<CityDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }

        public async Task<IPasswordHasher<User>> GetPassworhHasher()
        {
            var mock = new Mock<IPasswordHasher<User>>();
            mock.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((a, b) => b);

            return await Task.Run(() => mock.Object);
        }

        /// <summary>
        ///  Добавляет админа (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddAdmin_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            var admin = new UserRegisterModel
            {
                Name = "newadmin",
                SurName = "surAdmin",
                Email = "newadmin@mail.ru",
                PhoneNumber = "adminphone",
                Password = "admin",
                RoleId = RolesOptions.Admin,
                NameCity = city.Name
            };

            //act
            var id = await _service.AddAsync(admin);
            var resultAdmin = await _context.Users.SingleOrDefaultAsync(x => x.UserGuid == id);
            //assert
            Assert.AreEqual(admin.Name, resultAdmin.Name);
            Assert.AreEqual(admin.Email, resultAdmin.Email);
            Assert.AreEqual(admin.Password + resultAdmin.PasswordSalt, resultAdmin.PasswordHash);
            Assert.AreEqual(admin.PhoneNumber, resultAdmin.Phone);
            Assert.AreEqual(admin.SurName, resultAdmin.SurName);
            Assert.AreEqual(admin.RoleId, resultAdmin.RoleId);
        }

        /// <summary>
        ///  Добавляет соискателя (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddJobSeeker_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            var jobSeeker = new JobSeekerRegisterModel()
            {
                Name = "newjobSeeker",
                SurName = "surjobSeeker",
                Email = "newjobSeeker@mail.ru",
                PhoneNumber = "jobSeeker",
                Password = "jobSeeker",
                NameCity = city.Name,
                Citizenship = "Рф",
                DateOfBirth = DateTime.Now
            };

            //act
            var id = await _service.AddAsync(jobSeeker);
            var resultJobSeeker = await _context.JobSeekers.SingleOrDefaultAsync(x => x.UserGuid == id);
            //assert
            Assert.AreEqual(jobSeeker.Name, resultJobSeeker.Name);
            Assert.AreEqual(jobSeeker.Email, resultJobSeeker.Email);
            Assert.AreEqual(jobSeeker.Password + resultJobSeeker.PasswordSalt, resultJobSeeker.PasswordHash);
            Assert.AreEqual(jobSeeker.PhoneNumber, resultJobSeeker.Phone);
            Assert.AreEqual(jobSeeker.SurName, resultJobSeeker.SurName);
            Assert.AreEqual(jobSeeker.Citizenship, resultJobSeeker.Citizenship);
            Assert.AreEqual(jobSeeker.DateOfBirth, resultJobSeeker.DateOfBirth);
        }

        /// <summary>
        ///  Добавляет работодателя (успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddEmployer_Success()
        {
            //Arrange
            var city = await _context.Cities.FirstAsync();
            var employer = new EmployerRegisterModel
            {
                Name = "newemployer",
                SurName = "suremployer",
                Email = "newemployer@mail.ru",
                PhoneNumber = "employer",
                Password = "employer",
                NameCity = city.Name,
                NameCompany = "Компания",
                WebSite = "Сайтик"
            };

            //act
            var id = await _service.AddAsync(employer);
            var resultJobSeeker = await _context.Employers.SingleOrDefaultAsync(x => x.UserGuid == id);
            //assert
            Assert.AreEqual(employer.Name, resultJobSeeker.Name);
            Assert.AreEqual(employer.Email, resultJobSeeker.Email);
            Assert.AreEqual(employer.Password + resultJobSeeker.PasswordSalt, resultJobSeeker.PasswordHash);
            Assert.AreEqual(employer.PhoneNumber, resultJobSeeker.Phone);
            Assert.AreEqual(employer.SurName, resultJobSeeker.SurName);
            Assert.AreEqual(employer.NameCompany, resultJobSeeker.NameOfCompany);
            Assert.AreEqual(employer.WebSite, resultJobSeeker.Site);
        }

        /// <summary>
        ///  Удаляет пользователя(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteUser_Success()
        {
            //Arrange
            var resultUser = await _context.Users.FirstAsync();
            //act
            await _service.DeleteAsync(resultUser.UserGuid);
            var actualUser = await _context.Users.SingleOrDefaultAsync(x => x.UserGuid == resultUser.UserGuid);
            //assert
            Assert.IsNull(actualUser);
        }

        /// <summary>
        /// Получает всех пользователей(успешно)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetUser_Success()
        {
            //act
            var resultUsers= await _service.GetAsync();
            resultUsers = resultUsers.OrderBy(x => x.Email).ToList();
            //assert
            CollectionAssert.AreEqual(_listTest, resultUsers);
        }
    }
}
